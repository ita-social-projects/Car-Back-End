using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IPreferencesService
    {
        UserPreferences GetPreferences(int userId);

        UserPreferences UpdatePreferences(UserPreferences preferences);
    }
}
