using System;
using System.Collections.Generic;
using System.Text;
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
        private Mock<IPreferencesService> _preferencesService;
        private UserPreferencesController _userPreferencesController;

        public User GetTestUser()
        {
            return new User()
            {
                Id = It.IsAny<int>(),
                Name = It.IsAny<string>(),
            };
        }

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
            (result as OkObjectResult).Value.Should().BeOfType<UserPreferences>();
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
            (result as OkObjectResult).Value.Should().BeNull();
        }

    }
}
