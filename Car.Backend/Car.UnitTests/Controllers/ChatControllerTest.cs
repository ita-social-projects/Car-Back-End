using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class ChatControllerTest
    {
        private readonly Mock<IChatService> chatService;
        private readonly ChatController chatController;
        private readonly Fixture fixture;

        public ChatControllerTest()
        {
            chatService = new Mock<IChatService>();
            chatController = new ChatController(chatService.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetUserChats_WhenUserExists_ReturnsOkObjectResult()
        {
            // Arrange
            var user = fixture.Create<User>();
            var chats = fixture.Create<List<Chat>>();

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
            var user = fixture.Create<User>();
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