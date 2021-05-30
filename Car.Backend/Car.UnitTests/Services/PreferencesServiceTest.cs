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
        public async Task GetPreferencesAsync_WhenPreferencesNotExist_ReturnsNull()
        {
            // Arrange
            var preferences = Fixture.Create<List<UserPreferences>>();
            var userPreferences = Fixture.Create<UserPreferences>();

            preferencesRepository.Setup(r => r.Query())
                .Returns(preferences.AsQueryable().BuildMock().Object);

            // Act
            var result = await preferencesService.GetPreferencesAsync(userPreferences.Id);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdatePreferences_WhenPreferencesIsValid_ReturnsPreferencesObject()
        {
            // Arrange
            var preferencesDTO = Fixture.Build<UserPreferencesDTO>().Create();
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

        [Fact]
        public async Task UpdatePreferences_WhenPreferencesIsNotValid_ReturnsNull()
        {
            // Arrange
            var preferences = Fixture.Create<UserPreferences>();
            var preferencesDTO = Fixture.Create<UserPreferencesDTO>();

            preferencesRepository.Setup(r => r.UpdateAsync(preferences))
                .ReturnsAsync((UserPreferences)null);

            // Act
            var result = await preferencesService.UpdatePreferencesAsync(preferencesDTO);

            // Assert
            result.Should().BeNull();
        }
    }
}
