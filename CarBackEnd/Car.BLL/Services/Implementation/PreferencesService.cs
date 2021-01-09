using Car.DAL.Interfaces;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using System.Linq;

namespace Car.BLL.Services.Implementation
{
    public class PreferencesService : IPreferencesService
    {
        private readonly IUnitOfWork<UserPreferences> _unitOfWork;

        public PreferencesService(IUnitOfWork<UserPreferences> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserPreferences GetPreferences(int userId)
        {
            return _unitOfWork.GetRepository().Query().Where(p => p.UserId == userId).FirstOrDefault();
        }

        public UserPreferences UpdatePreferences(UserPreferences preferences)
        {
            _unitOfWork.GetRepository().Update(preferences);
            _unitOfWork.SaveChanges();
            return preferences;
        }
    }
}
