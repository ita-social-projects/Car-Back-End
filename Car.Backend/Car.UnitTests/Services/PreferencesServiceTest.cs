using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
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

        [Theory]
        [AutoEntityData]
        public async Task GetPreferencesAsync_WhenPreferencesExist_ReturnsPreferencesObject(
            List<UserPreferences> preferences, UserPreferences userPreferences)
        {
            // Arrange
            preferences.Add(userPreferences);

            preferencesRepository.Setup(r => r.Query())
                .Returns(preferences.AsQueryable().BuildMock().Object);

            // Act
            var result = await preferencesService.GetPreferencesAsync(userPreferences.Id);

            // Assert
            result.Should().BeEquivalentTo(userPreferences);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetPreferencesAsync_WhenPreferencesNotExist_ReturnsNull(
             List<UserPreferences> preferences, UserPreferences userPreferences)
        {
            // Arrange
            preferencesRepository.Setup(r => r.Query())
                .Returns(preferences.AsQueryable().BuildMock().Object);

            // Act
            var result = await preferencesService.GetPreferencesAsync(userPreferences.Id);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdatePreferences_WhenPreferencesIsValid_ReturnsPreferencesObject(UserPreferencesDto preferencesDTO)
        {
            // Arrange
            var inputPreferences = Fixture.Build<UserPreferences>()
                .With(p => p.Id, preferencesDTO.Id)
                .Create();

            preferencesRepository.Setup(repo => repo.GetByIdAsync(preferencesDTO.Id))
                .ReturnsAsync(inputPreferences);

            // Act
            var result = await preferencesService.UpdatePreferencesAsync(preferencesDTO);

            // Assert
            result.Should().BeEquivalentTo(preferencesDTO, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdatePreferences_WhenPreferencesIsNotValid_ReturnsNull(UserPreferences preferences, UserPreferencesDto preferencesDTO)
        {
            // Arrange
            preferencesRepository.Setup(r => r.UpdateAsync(preferences))
                .ReturnsAsync((UserPreferences)null);

            // Act
            var result = await preferencesService.UpdatePreferencesAsync(preferencesDTO);

            // Assert
            result.Should().BeNull();
        }
    }
}
