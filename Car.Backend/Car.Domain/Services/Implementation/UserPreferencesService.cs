using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class UserPreferencesService : IUserPreferencesService
    {
        private readonly IRepository<UserPreferences> preferencesRepository;
        private readonly IMapper mapper;

        public UserPreferencesService(IRepository<UserPreferences> preferencesRepository, IMapper mapper)
        {
            this.preferencesRepository = preferencesRepository;
            this.mapper = mapper;
        }

        public async Task<UserPreferencesDto> GetPreferencesAsync(int userId)
        {
            var preferences = await preferencesRepository.Query().FirstOrDefaultAsync(p => p.Id == userId);

            return mapper.Map<UserPreferences, UserPreferencesDto>(preferences);
        }

        public async Task<UserPreferencesDto?> UpdatePreferencesAsync(UserPreferencesDto preferencesDTO)
        {
            var preferences = await preferencesRepository.GetByIdAsync(preferencesDTO.Id);

            if (preferences != null)
            {
                preferences.DoAllowSmoking = preferencesDTO.DoAllowSmoking;
                preferences.DoAllowEating = preferencesDTO.DoAllowEating;
                preferences.Comments = preferencesDTO.Comments;
            }

            await preferencesRepository.SaveChangesAsync();

            return mapper.Map<UserPreferences, UserPreferencesDto>(preferences);
        }
    }
}
