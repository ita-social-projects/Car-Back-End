using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class PreferencesServiceTest
    {
        private readonly IPreferencesService preferencesService;
        private readonly Mock<IRepository<UserPreferences>> repository;
        private readonly Mock<IUnitOfWork<UserPreferences>> unitOfWork;

        public PreferencesServiceTest()
        {
            repository = new Mock<IRepository<UserPreferences>>();
            unitOfWork = new Mock<IUnitOfWork<UserPreferences>>();

            preferencesService = new PreferencesService(unitOfWork.Object);
        }

        public UserPreferences GetTestPreferences() =>
            new UserPreferences()
            {
                Id = It.IsAny<int>(),
                Comments = It.IsAny<string>(),
                DoAllowEating = It.IsAny<bool>(),
                DoAllowSmoking = It.IsAny<bool>(),
                UserId = It.IsAny<int>(),
            };

        [Fact]
        public void TestGetPreferences_WhenPreferenceExists()
        {
            var preferences = GetTestPreferences();

            repository.Setup(repository => repository.GetById(preferences.Id))
                .Returns(preferences);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            preferencesService.GetPreferences(preferences.UserId).Should().NotBeEquivalentTo(preferences);
        }

        [Fact]
        public void TestUpdatePreferences()
        {
            var preferences = GetTestPreferences();
            repository.Setup(repository => repository.GetById(preferences.Id))
               .Returns(preferences);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            preferencesService.UpdatePreferences(preferences).Should().BeEquivalentTo(preferences);
        }

        [Fact]
        public void TestUpdatePreferences_WhenNotExist()
        {
            var preferences = GetTestPreferences();
            repository.Setup(repository => repository.GetById(preferences.Id))
               .Returns(preferences);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            preferencesService.UpdatePreferences(preferences).Should().NotBeNull();
        }
    }
}
