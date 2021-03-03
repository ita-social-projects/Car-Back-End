using System.Collections.Generic;
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
    public class ChatControllerTest : TestBase
    {
        private readonly Mock<IChatService> chatService;
        private readonly ChatController chatController;

        public ChatControllerTest()
        {
            chatService = new Mock<IChatService>();
            chatController = new ChatController(chatService.Object);
        }

        [Fact]
        public async Task GetUserChats_WhenUserExists_ReturnsOkObjectResult()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var chats = Fixture.Create<List<Chat>>();

            chatService.Setup(x => x.GetUserChatsAsync(It.IsAny<int>()))
                .ReturnsAsync(chats);

            // Act
            var result = await chatController.GetUserChats(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeEquivalentTo(chats);
        }

        [Fact]
        public async Task GetUserChats_WhenUserNotExist_ReturnsOkObjectResult()
        {
            // Arrange
            var user = Fixture.Create<User>();
            chatService.Setup(x => x.GetUserChatsAsync(It.IsAny<int>()))
                .ReturnsAsync((List<Chat>)null);

            // Act
            var result = await chatController.GetUserChats(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }
    }
}