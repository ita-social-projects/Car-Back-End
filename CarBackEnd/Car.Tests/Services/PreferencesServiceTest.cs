using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Moq;
using Xunit;

namespace Car.Tests.Services
{
    public class PreferencesServiceTest
    {
        private IPreferencesService _preferencesService;
        private Mock<IRepository<UserPreferences>> _repository;
        private Mock<IUnitOfWork<UserPreferences>> _unitOfWork;

        public PreferencesServiceTest()
        {
            _repository = new Mock<IRepository<UserPreferences>>();
            _unitOfWork = new Mock<IUnitOfWork<UserPreferences>>();

            _preferencesService = new PreferencesService(_unitOfWork.Object);
        }

        public UserPreferences GetTestPreferences()
        {
            return new UserPreferences()
            {
                Id = 44,
                Comments = "What a lovely day!",
                DoAllowEating = false,
                DoAllowSmoking = true,
                UserId = 13,
            };
        }

        [Fact]
        public void TestGetPreferences_WhenPreferenceExists()
        {
            var preferences = GetTestPreferences();

            _repository.Setup(repository => repository.GetById(preferences.Id))
                .Returns(preferences);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Assert.NotEqual(preferences, _preferencesService.GetPreferences(preferences.UserId));
        }

        [Fact]
        public void TestUpdatePreferences()
        {
            var preferences = GetTestPreferences();
            _repository.Setup(repository => repository.GetById(preferences.Id))
               .Returns(preferences);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Assert.Equal(preferences, _preferencesService.UpdatePreferences(preferences));
        }

        [Fact]
        public void TestUpdatePreferences_WhenNotExist()
        {
            var preferences = GetTestPreferences();
            _repository.Setup(repository => repository.GetById(preferences.Id))
               .Returns(preferences);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Assert.NotNull(_preferencesService.UpdatePreferences(preferences));
        }
    }
}
