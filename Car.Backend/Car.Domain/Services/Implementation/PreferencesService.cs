using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class PreferencesService : IPreferencesService
    {
        private readonly IRepository<UserPreferences> preferencesRepository;

        public PreferencesService(IRepository<UserPreferences> preferencesRepository) =>
            this.preferencesRepository = preferencesRepository;

        public Task<UserPreferences> GetPreferencesAsync(int userId) =>
            preferencesRepository.Query().FirstOrDefaultAsync(p => p.Id == userId);

        public async Task<UserPreferences> UpdatePreferencesAsync(UserPreferencesDTO preferencesDTO)
        {
            var preferences = await preferencesRepository.GetByIdAsync(preferencesDTO.Id);

            if (preferences != null)
            {
                preferences.DoAllowSmoking = preferencesDTO.DoAllowSmoking;
                preferences.DoAllowEating = preferencesDTO.DoAllowEating;
                preferences.Comments = preferencesDTO.Comments;
            }

            await preferencesRepository.SaveChangesAsync();

            return preferences;
        }
    }
}
