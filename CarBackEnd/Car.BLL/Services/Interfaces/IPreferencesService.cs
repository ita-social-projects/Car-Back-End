using Car.DAL.Entities;

namespace Car.BLL.Services.Interfaces
{
    public interface IPreferencesService
    {
        UserPreferences GetPreferences(int userId);

        UserPreferences UpdatePreferences(UserPreferences preferences);
    }
}
