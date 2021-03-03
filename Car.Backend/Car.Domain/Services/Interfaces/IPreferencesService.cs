using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IPreferencesService
    {
        Task<UserPreferences> GetPreferencesAsync(int userId);

        Task<UserPreferences> UpdatePreferencesAsync(UserPreferences preferences);
    }
}
