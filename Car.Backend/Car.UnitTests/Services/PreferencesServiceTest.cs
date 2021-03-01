using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly Mock<IRepository<UserPreferences>> preferencesRepository;
        private readonly Fixture fixture;

        public PreferencesServiceTest()
        {
            preferencesRepository = new Mock<IRepository<UserPreferences>>();

            preferencesService = new PreferencesService(preferencesRepository.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetPreferencesAsync_WhenPreferencesExist_ReturnsPreferencesObject()
        {
            // Arrange
            var preferences = fixture.Create<List<UserPreferences>>();
            var userPreferences = fixture.Create<UserPreferences>();
            preferences.Add(userPreferences);

            preferencesRepository.Setup(r => r.Query())
                .Returns(preferences.AsQueryable);

            // Act
            var result = await preferencesService.GetPreferencesAsync(userPreferences.Id);

            // Assert
            result.Should().BeEquivalentTo(preferences);
        }

        [Fact]
        public async Task UpdatePreferences_WhenPreferencesIsValid_ReturnsPreferencesObject()
        {
            // Arrange
            var preferences = fixture.Create<UserPreferences>();

            preferencesRepository.Setup(r => r.UpdateAsync(preferences))
               .ReturnsAsync(preferences);

            // Act
            var result = await preferencesService.UpdatePreferencesAsync(preferences);

            // Assert
            result.Should().BeEquivalentTo(preferences);
        }
    }
}
