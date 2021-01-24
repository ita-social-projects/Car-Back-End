using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class PreferencesService : IPreferencesService
    {
        private readonly IUnitOfWork<UserPreferences> unitOfWork;

        public PreferencesService(IUnitOfWork<UserPreferences> unitOfWork) => this.unitOfWork = unitOfWork;

        public UserPreferences GetPreferences(int userId) =>
            unitOfWork.GetRepository().Query().FirstOrDefault(p => p.UserId == userId);

        public UserPreferences UpdatePreferences(UserPreferences preferences)
        {
            unitOfWork.GetRepository().Update(preferences);
            unitOfWork.SaveChanges();
            return preferences;
        }
    }
}
