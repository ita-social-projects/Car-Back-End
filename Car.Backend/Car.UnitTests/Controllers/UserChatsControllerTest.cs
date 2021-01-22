using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Controllers;
using Car.Data.Entities;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
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

        public User GetTestUser()
        {
            return new User { Id = It.IsAny<int>(), Name = It.IsAny<string>() };
        }

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
    }
}