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
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class JourneyUserService : IJourneyUserService
    {
        private readonly IRepository<JourneyUser> journeyUserRepository;
        private readonly IMapper mapper;

        public JourneyUserService(
            IRepository<JourneyUser> journeyUserRepository,
            IMapper mapper)
        {
            this.journeyUserRepository = journeyUserRepository;
            this.mapper = mapper;
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

        public async Task<JourneyUserDto> UpdateJourneyUserAsync(JourneyUserDto updateJourneyUserDto)
        {
            var journeyUser = await journeyUserRepository
                .Query()
                .FirstOrDefaultAsync(ju => ju.JourneyId == updateJourneyUserDto.JourneyId
                                           && ju.UserId == updateJourneyUserDto.UserId);
            var updatedJourneyUser = mapper.Map<JourneyUserDto, JourneyUser>(updateJourneyUserDto);

            if (journeyUser is not null)
            {
                journeyUserRepository.Detach(journeyUser);
                journeyUser = await journeyUserRepository.UpdateAsync(updatedJourneyUser);
                await journeyUserRepository.SaveChangesAsync();
            }

            return mapper.Map<JourneyUser, JourneyUserDto>(journeyUser);
        }

        public async Task<JourneyUserDto> SetWithBaggageAsync(int journeyId, int userId, bool withBaggage)
        {
            var journeyUser = await journeyUserRepository
                .Query()
                .FirstOrDefaultAsync(ju => ju.JourneyId == journeyId && ju.UserId == userId);

            if (journeyUser is not null)
            {
                journeyUser.WithBaggage = withBaggage;
                await journeyUserRepository.SaveChangesAsync();
            }

            return mapper.Map<JourneyUser, JourneyUserDto>(journeyUser);
        }
    }
}
