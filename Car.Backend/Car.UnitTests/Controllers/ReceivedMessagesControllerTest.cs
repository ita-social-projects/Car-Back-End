using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Domain.Services;
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
    public class ReceivedMessagesControllerTest : TestBase
    {
        private readonly Mock<IReceivedMessagesService> receivedMessagesService;
        private readonly ReceivedMessagesController receivedMessagesController;

        public ReceivedMessagesControllerTest()
        {
            receivedMessagesService = new Mock<IReceivedMessagesService>();
            receivedMessagesController = new ReceivedMessagesController(receivedMessagesService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task MarkAsRead_ReturnsInt(int chatId)
        {
            // Arrange
            receivedMessagesService.Setup(rm => rm.MarkMessagesReadInChatAsync(chatId)).ReturnsAsync(0);

            // Act
            var result = await receivedMessagesController.MarkMessagesRead(chatId);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<int>();
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUnreadMessageForChat_ReturnsInt(int userId, int chatId)
        {
            // Arrange
            receivedMessagesService.Setup(rm => rm.GetUnreadMessageForChatAsync(chatId)).ReturnsAsync(0);

            // Act
            var result = await receivedMessagesController.GetUnreadMessageForChat(userId, chatId);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<int>();
            }
        }
    }
}
