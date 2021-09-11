using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class ReceivedMessagesServiceTest : TestBase
    {
        private readonly IReceivedMessagesService receivedMessagesService;
        private readonly Mock<IRepository<ReceivedMessages>> receivedMessagesRepository;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;

        public ReceivedMessagesServiceTest()
        {
            receivedMessagesRepository = new Mock<IRepository<ReceivedMessages>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            receivedMessagesService = new ReceivedMessagesService(
                receivedMessagesRepository.Object,
                httpContextAccessor.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task MarkMessagesReadInChatAsync_AllCorrect_ReturnsZero(int userId, int chatId)
        {
            // Arrange
            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.ChatId, chatId)
                .With(rm => rm.UserId, userId)
                .CreateMany(1);

            receivedMessagesRepository.Setup(repo => repo.Query()).Returns(receivedMessages.AsQueryable().BuildMock().Object);

            // Act
            var result = await receivedMessagesService.MarkMessagesReadInChatAsync(userId, chatId);

            // Assert
            result.Should().Be(default(int));
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUnreadMessageForChatAsync_AllCorrect_ReturnCorrectValue(int userId, int chatId)
        {
            // Arrange
            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.ChatId, chatId)
                .With(rm => rm.UserId, userId)
                .CreateMany(1)
                .ToList();

            receivedMessagesRepository.Setup(repo => repo.Query())
                .Returns(receivedMessages.AsQueryable().BuildMock().Object);

            var expected = receivedMessages.FirstOrDefault()!.UnreadMessagesCount;

            // Act
            var result = await receivedMessagesService.GetUnreadMessageForChatAsync(userId, chatId);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllUnreadMessages_Returns_messageCount_from_all_chats(User user)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.UserId, user.Id)
                .CreateMany(3)
                .ToList();

            receivedMessagesRepository
                .Setup(repo => repo.Query())
                .Returns(receivedMessages.AsQueryable().BuildMock().Object);

            var expected = receivedMessages
                .Where(rm => rm.UserId == user.Id)
                .Sum(rm => rm.UnreadMessagesCount);

            // Act
            var result = await receivedMessagesService.GetAllUnreadMessagesNumber();

            // Assert
            result.Should().Be(expected);
        }
    }
}
