using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
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
        private readonly Fixture fixture;

        public PreferencesServiceTest()
        {
            repository = new Mock<IRepository<UserPreferences>>();
            unitOfWork = new Mock<IUnitOfWork<UserPreferences>>();

            preferencesService = new PreferencesService(unitOfWork.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetPreferences_WhenPreferenceExists()
        {
            var preferences = fixture.Create<UserPreferences>();

            repository.Setup(r => r.GetById(preferences.Id))
                .Returns(preferences);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            preferencesService.GetPreferences(preferences.Id).Should().NotBeEquivalentTo(preferences);
        }

        [Fact]
        public void TestUpdatePreferences()
        {
            var preferences = fixture.Create<UserPreferences>();
            repository.Setup(r => r.GetById(preferences.Id))
               .Returns(preferences);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            preferencesService.UpdatePreferences(preferences).Should().BeEquivalentTo(preferences);
        }

        [Fact]
        public void TestUpdatePreferences_WhenNotExist()
        {
            var preferences = fixture.Create<UserPreferences>();
            repository.Setup(r => r.GetById(preferences.Id))
               .Returns(preferences);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            preferencesService.UpdatePreferences(preferences).Should().NotBeNull();
        }
    }
}
