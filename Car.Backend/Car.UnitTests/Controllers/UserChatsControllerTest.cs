namespace Car.UnitTests.Controllers
{
    using System.Collections.Generic;
    using Car.Data.Entities;
    using Car.Domain.Services.Interfaces;
    using Car.WebApi.Controllers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class UserChatsControllerTest
    {
        private readonly Mock<IUserChatsManager> userChatsManager;
        private readonly UserChatsController userChatsController;

        public UserChatsControllerTest()
        {
            this.userChatsManager = new Mock<IUserChatsManager>();
            this.userChatsController = new UserChatsController(this.userChatsManager.Object);
        }

        public User GetTestUser() =>
            new User
            {
                Id = It.IsAny<int>(),
                Name = It.IsAny<string>(),
            };

        [Fact]
        public void GetUserChats_WhenUserExists_ReturnsChatObject()
        {
            // Arrange
            var user = this.GetTestUser();
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
            var user = this.GetTestUser();
            this.userChatsManager.Setup(x => x.GetUsersChats(It.IsAny<int>())).Returns((List<Chat>)null);

            // Act
            var result = this.userChatsController.GetUserChats(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }
    }
}