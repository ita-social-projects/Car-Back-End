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
    public class UserPreferencesService : IUserPreferencesService
    {
        private readonly IRepository<UserPreferences> preferencesRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserPreferencesService(IRepository<UserPreferences> preferencesRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.preferencesRepository = preferencesRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserPreferencesDto?> GetPreferencesAsync(int userId)
        {
            var preferences = await preferencesRepository.Query().FirstOrDefaultAsync(p => p.Id == userId);

            return mapper.Map<UserPreferences, UserPreferencesDto>(preferences);
        }

        public async Task<(bool IsUpdated, UserPreferencesDto? UpdatedReferencesDto)> UpdatePreferencesAsync(UserPreferencesDto preferencesDTO)
        {
            var preferences = await preferencesRepository.GetByIdAsync(preferencesDTO.Id);

            if (preferences != null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();

                if (userId != preferences.Id)
                {
                    return (false, null);
                }

                preferences.DoAllowSmoking = preferencesDTO.DoAllowSmoking;
                preferences.DoAllowEating = preferencesDTO.DoAllowEating;
                preferences.Comments = preferencesDTO.Comments;
            }

            await preferencesRepository.SaveChangesAsync();

            return (true, mapper.Map<UserPreferences, UserPreferencesDto>(preferences!));
        }
    }
}
