using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class UserPreferencesControllerTest : TestBase
    {
        private readonly Mock<IUserPreferencesService> preferencesService;
        private readonly UserPreferencesController userPreferencesController;

        public UserPreferencesControllerTest()
        {
            preferencesService = new Mock<IUserPreferencesService>();
            userPreferencesController = new UserPreferencesController(preferencesService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetPreferences_WhenUserExists_ReturnsPreferencesObject(User user)
        {
            // Arrange
            var preferences = Mapper.Map<UserPreferences, UserPreferencesDto>(user.UserPreferences);
            preferencesService.Setup(x => x.GetPreferencesAsync(It.IsAny<int>()))
                .ReturnsAsync(preferences);

            // Act
            var result = await userPreferencesController.GetPreferences(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<UserPreferencesDto>();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetPreferences_WhenUserNotExist_ReturnsNull(User user)
        {
            // Arrange
            preferencesService.Setup(x => x.GetPreferencesAsync(It.IsAny<int>()))
                .ReturnsAsync((UserPreferencesDto)null);

            // Act
            var result = await userPreferencesController.GetPreferences(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdatePreferences_WhenUserPreferencesExists_ReturnsUserPreferences(
            UserPreferencesDto userPreferencesDTO)
        {
            // Arrange
            preferencesService.Setup(u => u.UpdatePreferencesAsync(userPreferencesDTO))
                .ReturnsAsync(userPreferencesDTO);

            // Act
            var result = await userPreferencesController.UpdatePreferences(userPreferencesDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<UserPreferencesDto>();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdatePreferences_WhenUserPreferencesNotExist_ReturnsNull(UserPreferencesDto userPreferences)
        {
            // Arrange
            preferencesService.Setup(u => u.UpdatePreferencesAsync(userPreferences))
                .ReturnsAsync((UserPreferencesDto)null);

            // Act
            var result = await userPreferencesController.UpdatePreferences(userPreferences);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }
    }
}
