using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class ReceivedMessagesControllerTest : TestBase
    {
        private readonly Mock<IReceivedMessagesService> receivedMessagesService;
        private readonly ReceivedMessagesController receivedMessagesController;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;

        public ReceivedMessagesControllerTest()
        {
            receivedMessagesService = new Mock<IReceivedMessagesService>();
            receivedMessagesController = new ReceivedMessagesController(receivedMessagesService.Object);
            httpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        [Theory]
        [AutoEntityData]
        public async Task MarkAsRead_IsAllowed_ReturnsOkObjectResut(int chatId)
        {
            // Arrange
            receivedMessagesService.Setup(rm => rm.MarkMessagesReadInChatAsync(chatId)).ReturnsAsync(true);

            // Act
            var result = await receivedMessagesController.MarkMessagesRead(chatId);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task MarkAsRead_IsNotAllowed_ReturnsForbidObject(int chatId)
        {
            // Arrange
            receivedMessagesService.Setup(rm => rm.MarkMessagesReadInChatAsync(chatId)).ReturnsAsync(false);

            // Act
            var result = await receivedMessagesController.MarkMessagesRead(chatId);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllUnreadMessagesNumber_ReReturnsOkObjectResult(User user, int unreadMessageCount)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            receivedMessagesService.Setup(s => s.GetAllUnreadMessagesNumber())
                .ReturnsAsync(unreadMessageCount);

            // Act
            var result = await receivedMessagesController.GetAllUnreadMessagesNumber();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(unreadMessageCount);
            }
        }
    }
}
