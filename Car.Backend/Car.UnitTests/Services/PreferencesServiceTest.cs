using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class PreferencesServiceTest : TestBase
    {
        private readonly IPreferencesService preferencesService;
        private readonly Mock<IRepository<UserPreferences>> preferencesRepository;

        public PreferencesServiceTest()
        {
            preferencesRepository = new Mock<IRepository<UserPreferences>>();
            preferencesService = new PreferencesService(preferencesRepository.Object);
        }

        [Fact]
        public async Task GetPreferencesAsync_WhenPreferencesExist_ReturnsPreferencesObject()
        {
            // Arrange
            var preferences = Fixture.Create<List<UserPreferences>>();
            var userPreferences = Fixture.Create<UserPreferences>();
            preferences.Add(userPreferences);

            preferencesRepository.Setup(r => r.Query())
                .Returns(preferences.AsQueryable().BuildMock().Object);

            // Act
            var result = await preferencesService.GetPreferencesAsync(userPreferences.Id);

            // Assert
            result.Should().BeEquivalentTo(userPreferences);
        }

        [Fact]
        public async Task UpdatePreferences_WhenPreferencesIsValid_ReturnsPreferencesObject()
        {
            // Arrange
            var preferences = Fixture.Create<UserPreferences>();

            preferencesRepository.Setup(r => r.UpdateAsync(preferences))
               .ReturnsAsync(preferences);

            // Act
            var result = await preferencesService.UpdatePreferencesAsync(preferences);

            // Assert
            result.Should().BeEquivalentTo(preferences);
        }
    }
}
