using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class UserPreferencesControllerTest
    {
        private readonly Mock<IPreferencesService> preferencesService;
        private readonly UserPreferencesController userPreferencesController;
        private readonly Fixture fixture;

        public UserPreferencesControllerTest()
        {
            preferencesService = new Mock<IPreferencesService>();
            userPreferencesController = new UserPreferencesController(preferencesService.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void GetPreferences_WhenUserExists_ReturnsUserObject()
        {
            // Arrange
            var user = fixture.Create<User>();
            preferencesService.Setup(x => x.GetPreferences(It.IsAny<int>())).Returns(new UserPreferences());

            // Act
            var result = userPreferencesController.GetPreferences(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<UserPreferences>();
        }

        [Fact]
        public void GetPreferences_WhenUserNotExists_ReturnsNull()
        {
            // Arrange
            var user = fixture.Create<User>();
            preferencesService.Setup(x => x.GetPreferences(It.IsAny<int>())).Returns((UserPreferences)null);

            // Act
            var result = userPreferencesController.GetPreferences(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }

        [Fact]
        public void UpdatePreferences_WhenUserPreferencesExists_ReturnsUpdatedUserPreferences()
        {
            var userPreferences = new UserPreferences();

            // Arrange
            preferencesService.Setup(u => u.UpdatePreferences(userPreferences)).Returns(userPreferences);

            // Act
            var result = userPreferencesController.UpdatePreferences(userPreferences);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<UserPreferences>();
        }

        [Fact]
        public void UpdatePreferences_WhenUserPreferencesNotExists_ReturnsNull()
        {
            var userPreferences = new UserPreferences();

            // Arrange
            preferencesService.Setup(u => u.UpdatePreferences(userPreferences)).Returns((UserPreferences)null);

            // Act
            var result = userPreferencesController.UpdatePreferences(userPreferences);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }
    }
}
