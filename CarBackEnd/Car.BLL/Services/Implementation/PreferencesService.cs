using System.Linq;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;

namespace Car.BLL.Services.Implementation
{
    public class PreferencesService : IPreferencesService
    {
        private readonly IUnitOfWork<UserPreferences> unitOfWork;

        public PreferencesService(IUnitOfWork<UserPreferences> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UserPreferences GetPreferences(int userId)
        {
            return unitOfWork.GetRepository().Query().Where(p => p.UserId == userId).FirstOrDefault();
        }

        public UserPreferences UpdatePreferences(UserPreferences preferences)
        {
            unitOfWork.GetRepository().Update(preferences);
            unitOfWork.SaveChanges();
            return preferences;
        }
    }
}
