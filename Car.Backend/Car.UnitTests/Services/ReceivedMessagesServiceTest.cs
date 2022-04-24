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
        private readonly Mock<IRepository<User>> userRepository;
        private readonly Mock<IRepository<Journey>> journeyRepository;

        public ReceivedMessagesServiceTest()
        {
            receivedMessagesRepository = new Mock<IRepository<ReceivedMessages>>();
            userRepository = new Mock<IRepository<User>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            journeyRepository = new Mock<IRepository<Journey>>();
            receivedMessagesService = new ReceivedMessagesService(
                receivedMessagesRepository.Object,
                userRepository.Object,
                httpContextAccessor.Object,
                Mapper,
                journeyRepository.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddReceivedMessages_ReceivedMessagesAddedCorrect(Chat chat)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(repo => repo.ChatId, chat.Id)
                .CreateMany(1)
                .ToList();
            journeyRepository.Setup(repo => repo
                .Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            receivedMessagesRepository
                .Setup(r => r.AddAsync(It.IsAny<ReceivedMessages>()));

            // Act
            await receivedMessagesService.AddReceivedMessages(chat);

            // Assert
            receivedMessagesRepository.Verify(mock => mock.AddAsync(It.IsAny<ReceivedMessages>()), Times.Once);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddReceivedMessages_ReceivedMessagesSavedCorrect(Chat chat)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(repo => repo.ChatId, chat.Id)
                .CreateMany(1)
                .ToList();
            journeyRepository.Setup(repo => repo
                .Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            receivedMessagesRepository
                .Setup(r => r.SaveChangesAsync());

            // Act
            await receivedMessagesService.AddReceivedMessages(chat);

            // Assert
            receivedMessagesRepository.Verify(mock => mock.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetReceivedMessages_ReceivedMessagesAreCorrect(Chat chat)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(repo => repo.ChatId, chat.Id)
                .CreateMany(1)
                .ToList();
            journeyRepository.Setup(repo => repo
                .Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await receivedMessagesService.GetReceivedMessages(chat.Id);

            // Assert
            result.UnreadMessagesCount.Should().Be(0);
        }

        [Theory]
        [AutoEntityData]
        public async Task MarkMessagesReadInChatAsync_IsAllowed_ReturnsTrue(int chatId, IEnumerable<User> users)
        {
            // Arrange
            var userList = users.ToList();
            var user = userList.First();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.ChatId, chatId)
                .With(rm => rm.UserId, user.Id)
                .CreateMany(1);

            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            receivedMessagesRepository.Setup(repo => repo.Query()).Returns(receivedMessages.AsQueryable().BuildMock().Object);

            // Act
            var result = await receivedMessagesService.MarkMessagesReadInChatAsync(chatId);

            // Assert
            result.Should().Be(true);
        }

        [Theory]
        [AutoEntityData]
        public async Task MarkMessagesReadInChatAsync_IsNotAllowed_ReturnsFalse(int chatId, IEnumerable<User> users)
        {
            // Arrange
            var userList = users.ToList();
            var user = userList.First();
            var currentUser = Fixture.Build<User>().With(u => u.Id, user.Id + 1).Create();
            var claims = new List<Claim>() { new("preferred_username", currentUser.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { currentUser }.AsQueryable());

            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.ChatId, chatId)
                .With(rm => rm.UserId, user.Id)
                .CreateMany(1);

            receivedMessagesRepository.Setup(repo => repo.Query()).Returns(receivedMessages.AsQueryable().BuildMock().Object);

            // Act
            var result = await receivedMessagesService.MarkMessagesReadInChatAsync(chatId);

            // Assert
            result.Should().Be(false);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllUnreadMessages_ThereAreSomeMessages_ReturnsValidSumOfUnreadMessages(User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.UserId, user.Id)
                .With(rm => rm.UnreadMessagesCount, 1)
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
