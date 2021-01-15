using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using FluentAssertions;
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
                Id = It.IsAny<int>(),
                Comments = It.IsAny<string>(),
                DoAllowEating = It.IsAny<bool>(),
                DoAllowSmoking = It.IsAny<bool>(),
                UserId = It.IsAny<int>(),
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

            _preferencesService.GetPreferences(preferences.UserId).Should().NotBeEquivalentTo(preferences);
        }

        [Fact]
        public void TestUpdatePreferences()
        {
            var preferences = GetTestPreferences();
            _repository.Setup(repository => repository.GetById(preferences.Id))
               .Returns(preferences);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            _preferencesService.UpdatePreferences(preferences).Should().BeEquivalentTo(preferences);
        }

        [Fact]
        public void TestUpdatePreferences_WhenNotExist()
        {
            var preferences = GetTestPreferences();
            _repository.Setup(repository => repository.GetById(preferences.Id))
               .Returns(preferences);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            _preferencesService.UpdatePreferences(preferences).Should().NotBeNull();
        }
    }
}
