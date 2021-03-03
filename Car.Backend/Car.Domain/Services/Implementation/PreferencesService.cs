using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
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

        public async Task<UserPreferences> UpdatePreferencesAsync(UserPreferences preferences)
        {
            var updatedPreferences = await preferencesRepository.UpdateAsync(preferences);
            await preferencesRepository.SaveChangesAsync();

            return updatedPreferences;
        }
    }
}
