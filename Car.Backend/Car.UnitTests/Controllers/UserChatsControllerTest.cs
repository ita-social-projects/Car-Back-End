using System.Collections.Generic;
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
    public class UserChatsControllerTest
    {
        private readonly Mock<IUserChatsManager> userChatsManager;
        private readonly UserChatsController userChatsController;

        public UserChatsControllerTest()
        {
            this.userChatsManager = new Mock<IUserChatsManager>();
            this.userChatsController = new UserChatsController(this.userChatsManager.Object);
        }

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void GetUserChats_WhenUserExists_ReturnsChatObject()
        {
            // Arrange
            var user = this.fixture.Create<User>();
            this.userChatsManager.Setup(x => x.GetUsersChats(It.IsAny<int>())).Returns(new List<Chat>());

            // Act
            var result = this.userChatsController.GetUserChats(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<List<Chat>>();
        }

        [Fact]
        public void GetUserChats_WhenUserNotExists_ReturnsNull()
        {
            // Arrange
            var user = this.fixture.Create<User>();
            this.userChatsManager.Setup(x => x.GetUsersChats(It.IsAny<int>())).Returns((List<Chat>)null);

            // Act
            var result = this.userChatsController.GetUserChats(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }
    }
}