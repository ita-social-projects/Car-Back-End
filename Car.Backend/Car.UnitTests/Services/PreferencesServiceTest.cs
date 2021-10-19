using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class PreferencesServiceTest : TestBase
    {
        private readonly IUserPreferencesService preferencesService;
        private readonly Mock<IRepository<UserPreferences>> preferencesRepository;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;

        public PreferencesServiceTest()
        {
            preferencesRepository = new Mock<IRepository<UserPreferences>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            preferencesService = new UserPreferencesService(preferencesRepository.Object, Mapper, httpContextAccessor.Object);
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

            var preference = Mapper.Map<UserPreferences, UserPreferencesDto>(userPreferences);

            // Act
            var result = await preferencesService.GetPreferencesAsync(userPreferences.Id);

            // Assert
            result.Should().BeEquivalentTo(preference);
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
        public async Task UpdatePreferences_WhenPreferencesIsValidAndIsAllowed_ReturnsPreferencesObject(UserPreferencesDto preferencesDTO)
        {
            // Arrange
            var inputPreferences = Fixture.Build<UserPreferences>()
                .With(p => p.Id, preferencesDTO.Id)
                .Create();

            preferencesRepository.Setup(repo => repo.GetByIdAsync(preferencesDTO.Id))
                .ReturnsAsync(inputPreferences);

            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, preferencesDTO.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            // Act
            var result = await preferencesService.UpdatePreferencesAsync(preferencesDTO);

            // Assert
            result.Should().BeEquivalentTo((true, preferencesDTO), options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdatePreferences_WhenPreferencesIsValidAndIsNotAllowed_ReturnsFalse(UserPreferencesDto preferencesDTO)
        {
            // Arrange
            var inputPreferences = Fixture.Build<UserPreferences>()
                .With(p => p.Id, preferencesDTO.Id)
                .Create();

            preferencesRepository.Setup(repo => repo.GetByIdAsync(preferencesDTO.Id))
                .ReturnsAsync(inputPreferences);

            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, (preferencesDTO.Id + 1).ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            // Act
            var result = await preferencesService.UpdatePreferencesAsync(preferencesDTO);

            // Assert
            result.Should().Be((false, null));
        }
        
        [Theory]
        [AutoEntityData]
        public async Task UpdatePreferences_WhenPreferencesIsNotValid_ReturnsNull(UserPreferences preferences, UserPreferencesDto preferencesDTO)
        {
            // Arrange
            preferencesRepository.Setup(r => r.UpdateAsync(preferences))
                .ReturnsAsync((UserPreferences)null);

            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, preferences.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            // Act
            var result = await preferencesService.UpdatePreferencesAsync(preferencesDTO);

            // Assert
            result.UpdatedReferencesDto.Should().BeNull();
        }
    }
}
