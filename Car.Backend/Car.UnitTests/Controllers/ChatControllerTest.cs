using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Dto.ChatDto;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
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
            var chats = Fixture.Create<List<ChatDto>>();

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
                .ReturnsAsync((List<ChatDto>)null);

            // Act
            var result = await chatController.GetUserChats(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }

        [Fact]
        public async Task GetChat_WhenChatExists_ReturnsOkObjectResult()
        {
            // Arrange
            var chat = Fixture.Create<IEnumerable<MessageDto>>();

            chatService.Setup(x => x.GetMessagesByChatIdAsync(chat.FirstOrDefault().ChatId, 0))
                .ReturnsAsync(chat);

            // Act
            var result = await chatController.GetChat(chat.FirstOrDefault().ChatId, 0);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(chat);
            }
        }

        [Fact]
        public async Task GetChat_WhenChatNotExist_ReturnsOkObjectResult()
        {
            // Arrange
            var chat = Fixture.Create<Chat>();

            chatService.Setup(x => x.GetMessagesByChatIdAsync(chat.Id, 0))
                .ReturnsAsync((IEnumerable<MessageDto>)null);

            // Act
            var result = await chatController.GetChat(chat.Id, 0);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task AddChat_WhenChatIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var chat = Fixture.Create<Chat>();
            var createChatDto = Mapper.Map<Chat, CreateChatDto>(chat);
            chatService.Setup(x => x.AddChatAsync(createChatDto)).ReturnsAsync(chat);

            // Act
            var result = await chatController.AddChat(createChatDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(chat);
            }
        }

        [Fact]
        public async Task AddMessage_WhenMessageIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var message = Fixture.Create<Message>();

            chatService.Setup(x => x.AddMessageAsync(message))
                .ReturnsAsync(message);

            // Act
            var result = await chatController.AddMessage(message);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(message);
            }
        }
    }
}