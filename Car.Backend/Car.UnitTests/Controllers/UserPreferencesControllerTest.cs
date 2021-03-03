using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
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
        private readonly Mock<IPreferencesService> preferencesService;
        private readonly UserPreferencesController userPreferencesController;

        public UserPreferencesControllerTest()
        {
            preferencesService = new Mock<IPreferencesService>();
            userPreferencesController = new UserPreferencesController(preferencesService.Object);
        }

        [Fact]
        public async Task GetPreferences_WhenUserExists_ReturnsPreferencesObject()
        {
            // Arrange
            var user = Fixture.Create<User>();
            preferencesService.Setup(x => x.GetPreferencesAsync(It.IsAny<int>()))
                .ReturnsAsync(user.UserPreferences);

            // Act
            var result = await userPreferencesController.GetPreferences(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<UserPreferences>();
        }

        [Fact]
        public async Task GetPreferences_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var user = Fixture.Create<User>();
            preferencesService.Setup(x => x.GetPreferencesAsync(It.IsAny<int>()))
                .ReturnsAsync((UserPreferences)null);

            // Act
            var result = await userPreferencesController.GetPreferences(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }

        [Fact]
        public async Task UpdatePreferences_WhenUserPreferencesExists_ReturnsUserPreferences()
        {
            // Arrange
            var userPreferences = Fixture.Create<UserPreferences>();

            preferencesService.Setup(u => u.UpdatePreferencesAsync(userPreferences))
                .ReturnsAsync(userPreferences);

            // Act
            var result = await userPreferencesController.UpdatePreferences(userPreferences);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<UserPreferences>();
        }

        [Fact]
        public async Task UpdatePreferences_WhenUserPreferencesNotExist_ReturnsNull()
        {
            // Arrange
            var userPreferences = new UserPreferences();

            preferencesService.Setup(u => u.UpdatePreferencesAsync(userPreferences))
                .ReturnsAsync((UserPreferences)null);

            // Act
            var result = await userPreferencesController.UpdatePreferences(userPreferences);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }
    }
}
