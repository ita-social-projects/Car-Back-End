using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IPreferencesService
    {
        Task<UserPreferences> GetPreferencesAsync(int userId);

        Task<UserPreferences> UpdatePreferencesAsync(UserPreferencesDTO preferencesDTO);
    }
}
