using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class JourneyUserService : IJourneyUserService
    {
        private readonly IRepository<JourneyUser> journeyUserRepository;
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public JourneyUserService(
            IRepository<JourneyUser> journeyUserRepository,
            IRepository<User> userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this.journeyUserRepository = journeyUserRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<JourneyUserDto> GetJourneyUserByIdAsync(int journeyId, int userId)
        {
            var journeyUser = await journeyUserRepository
                .Query()
                .FirstOrDefaultAsync(ju => ju.JourneyId == journeyId && ju.UserId == userId);

            return mapper.Map<JourneyUser, JourneyUserDto>(journeyUser);
        }

        public async Task<bool> HasBaggage(int journeyId, int userId)
        {
            var journeyUser = await journeyUserRepository
                .Query()
                .FirstOrDefaultAsync(ju => ju.JourneyId == journeyId && ju.UserId == userId);

            return journeyUser is not null && journeyUser.WithBaggage;
        }

        public async Task<(bool IsUpdated, JourneyUserDto? UpdatedJourneyUserDto)> UpdateJourneyUserAsync(JourneyUserDto updateJourneyUserDto)
        {
            var journeyUser = await journeyUserRepository
                .Query()
                .FirstOrDefaultAsync(ju => ju.JourneyId == updateJourneyUserDto.JourneyId
                                           && ju.UserId == updateJourneyUserDto.UserId);
            var updatedJourneyUser = mapper.Map<JourneyUserDto, JourneyUser>(updateJourneyUserDto);

            if (journeyUser is not null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (userId != journeyUser.UserId && userId != journeyUser?.Journey?.OrganizerId)
                {
                    return (false, null);
                }

                journeyUserRepository.Detach(journeyUser);
                journeyUser = await journeyUserRepository.UpdateAsync(updatedJourneyUser);
                await journeyUserRepository.SaveChangesAsync();
                return (true, mapper.Map<JourneyUser, JourneyUserDto>(journeyUser));
            }

            return (true, null);
        }

        public async Task<(bool IsUpdated, JourneyUserDto? UpdatedJourneyUserDto)> SetWithBaggageAsync(int journeyId, int userId, bool withBaggage)
        {
            var journeyUser = await journeyUserRepository
                .Query()
                .FirstOrDefaultAsync(ju => ju.JourneyId == journeyId && ju.UserId == userId);

            if (journeyUser is not null)
            {
                int currentUserId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (currentUserId != userId)
                {
                    return (false, null);
                }

                journeyUser.WithBaggage = withBaggage;
                await journeyUserRepository.SaveChangesAsync();
                return (true, mapper.Map<JourneyUser, JourneyUserDto>(journeyUser));
            }

            return (true, null);
        }
    }
}
