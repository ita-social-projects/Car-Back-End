using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class PreferencesService : IPreferencesService
    {
        private readonly IUnitOfWork<UserPreferences> _unitOfWork;

        public PreferencesService(IUnitOfWork<UserPreferences> unitOfWork) => _unitOfWork = unitOfWork;

        public UserPreferences GetPreferences(int userId) =>
            _unitOfWork.GetRepository().Query().FirstOrDefault(p => p.UserId == userId);

        public UserPreferences UpdatePreferences(UserPreferences preferences)
        {
            _unitOfWork.GetRepository().Update(preferences);
            _unitOfWork.SaveChanges();
            return preferences;
        }
    }
}
