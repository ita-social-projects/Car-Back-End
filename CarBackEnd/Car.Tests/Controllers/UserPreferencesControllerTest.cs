using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using CarBackEnd.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.Tests.Controllers
{
    public class UserPreferencesControllerTest
    {
        private readonly Mock<IPreferencesService> _preferencesService;
        private readonly UserPreferencesController _userPreferencesController;

        public User GetTestUser() =>
            new User()
            {
                Id = It.IsAny<int>(),
                Name = It.IsAny<string>(),
            };

        public UserPreferencesControllerTest()
        {
            _preferencesService = new Mock<IPreferencesService>();
            _userPreferencesController = new UserPreferencesController(_preferencesService.Object);
        }

        [Fact]
        public void GetPreferences_WhenUserExists_ReturnsUserObject()
        {
            // Arrange
            var user = GetTestUser();
            _preferencesService.Setup(x => x.GetPreferences(It.IsAny<int>())).Returns(new UserPreferences());

            // Act
            var result = _userPreferencesController.GetPreferences(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<UserPreferences>();
        }

        [Fact]
        public void GetPreferences_WhenUserNotExists_ReturnsNull()
        {
            // Arrange
            var user = GetTestUser();
            _preferencesService.Setup(x => x.GetPreferences(It.IsAny<int>())).Returns((UserPreferences)null);

            // Act
            var result = _userPreferencesController.GetPreferences(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }

        [Fact]
        public void UpdatePreferences_WhenUserPreferencesExists_ReturnsUpdatedUserPreferences()
        {
            var userPreferences = new UserPreferences();

            // Arrange
            _preferencesService.Setup(u => u.UpdatePreferences(userPreferences)).Returns(userPreferences);

            // Act
            var result = _userPreferencesController.UpdatePreferences(userPreferences);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<UserPreferences>();
        }

        [Fact]
        public void UpdatePreferences_WhenUserPreferencesNotExists_ReturnsNull()
        {
            var userPreferences = new UserPreferences();

            // Arrange
            _preferencesService.Setup(u => u.UpdatePreferences(userPreferences)).Returns((UserPreferences)null);

            // Act
            var result = _userPreferencesController.UpdatePreferences(userPreferences);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }
    }
}
