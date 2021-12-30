using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Dto.Chat;
using Car.Domain.Filters;
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
    public class ChatServiceTest : TestBase
    {
        private readonly IChatService chatService;
        private readonly Mock<IRepository<Chat>> chatRepository;
        private readonly Mock<IRepository<User>> userRepository;
        private readonly Mock<IRepository<Message>> messageRepository;
        private readonly Mock<IRepository<ReceivedMessages>> receivedMessagesRepository;
        private readonly Mock<IRepository<Journey>> journeyRepository;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly Mock<IReceivedMessagesService> receivedMessagesService;

        public ChatServiceTest()
        {
            chatRepository = new Mock<IRepository<Chat>>();
            userRepository = new Mock<IRepository<User>>();
            messageRepository = new Mock<IRepository<Message>>();
            receivedMessagesRepository = new Mock<IRepository<ReceivedMessages>>();
            journeyRepository = new Mock<IRepository<Journey>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            receivedMessagesService = new Mock<IReceivedMessagesService>();
            chatService = new ChatService(
                userRepository.Object,
                chatRepository.Object,
                messageRepository.Object,
                receivedMessagesRepository.Object,
                journeyRepository.Object,
                Mapper,
                httpContextAccessor.Object,
                receivedMessagesService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetChatByIdAsync_WhenChatExists_ReturnsCorrectType(IEnumerable<Message> chats)
        {
            // Arrange
            var chatList = chats.ToList();
            var chat = chatList.First();

            messageRepository.Setup(repo => repo
                    .Query(It.IsAny<Expression<Func<Message, object>>[]>()))
                .Returns(chatList.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetMessagesByChatIdAsync(chat.Id, chat.Id);

            // Assert
            result.Should().BeOfType<List<MessageDto>>();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetChatByIdAsync_WhenChatNotExist_ReturnsNull(IEnumerable<Message> chats)
        {
            // Arrange
            var chatList = chats.ToList();
            var id = chatList.Max(chat => chat.Id) + 1;

            messageRepository.Setup(repo => repo
                    .Query(It.IsAny<Expression<Func<Message, object>>[]>()))
                .Returns(chatList.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService
                .GetMessagesByChatIdAsync(id, chatList.First().Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task AddChatAsync_ChatIsCreated_ReturnsChatObject(JourneyDto journeyDto)
        {
            // Arrange
            Journey journey = Mapper.Map<JourneyDto, Journey>(journeyDto);

            var users = Fixture
                .Build<User>()
                .With(u => u.Id, journey.OrganizerId)
                .CreateMany(1);
            var user = users.First();
            var mock = users.AsQueryable().BuildMock();

            userRepository
                .Setup(rep => rep.Query())
                .Returns(mock.Object);

            var claims = new List<Claim>() { new("preferred_username", user.Email) };

            httpContextAccessor
                .Setup(h => h.HttpContext.User.Claims)
                .Returns(claims);

            var chat = Fixture.Build<ChatDto>()
                .With(chat => chat.Id, journey.ChatId)
                .Create();
            var chatEntity = Mapper.Map<ChatDto, Chat>(chat);
            var journeys = Fixture.Build<Journey>()
                .With(repo => repo.ChatId, chat.Id)
                .CreateMany(1)
                .ToList();

            journeyRepository.Setup(repo => repo
                .Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            chatRepository.Setup(repo => repo
                .AddAsync(It.IsAny<Chat>()))
                .ReturnsAsync(chatEntity);

            // Act
            var result = await chatService.AddChatAsync(journey.Id);

            // Assert
            result.Should().BeEquivalentTo(chatEntity);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddChatAsync_ChatIsNotCreated_ReturnsNull(JourneyDto journeyDto)
        {
            // Arrange
            Journey journey = Mapper.Map<JourneyDto, Journey>(journeyDto);

            var users = Fixture.Build<User>()
                .With(u => u.Id, journey.OrganizerId)
                .CreateMany(1);
            var user = users.First();
            var mock = users
                .AsQueryable()
                .BuildMock();

            userRepository
                .Setup(rep => rep.Query())
                .Returns(mock.Object);

            var claims = new List<Claim>() { new("preferred_username", user.Email) };

            httpContextAccessor
                .Setup(h => h.HttpContext.User.Claims)
                .Returns(claims);

            var chat = Fixture.Build<ChatDto>()
                .With(chat => chat.Id, journey.ChatId)
                .Create();

            var chatEntity = Mapper.Map<ChatDto, Chat>(chat);
            chatRepository.Setup(repo => repo
                .AddAsync(chatEntity))
                .ReturnsAsync((Chat)null);

            // Act
            var result = await chatService.AddChatAsync(journey.Id);

            // Assert
            chatRepository.Verify(repo => repo.AddAsync(chatEntity), Times.Never());
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetUserChatsAsync_WhenChatExist_ReturnsChatCollection()
        {
            // Arrange
            var messageFirst = Fixture.Build<Message>()
                .With(message => message.CreatedAt, new DateTime(2021, 12, 02))
                .Create();
            var chatFirst = Fixture.Build<Chat>()
                .With(chat => chat.Messages, new List<Message>() { messageFirst })
                .Create();
            var journeyFirst = Fixture.Build<Journey>()
                .With(journey => journey.Chat, chatFirst)
                .With(journey => journey.DepartureTime, new DateTime(2021, 12, 05))
                .Create();
            chatFirst.Journeys = new List<Journey>() { journeyFirst };

            var messageSecond = Fixture.Build<Message>()
                .With(message => message.CreatedAt, new DateTime(2021, 12, 03))
                .Create();
            var chatSecond = Fixture.Build<Chat>()
                .With(chat => chat.Messages, new List<Message>() { messageSecond })
                .Create();
            var journeySecond = Fixture.Build<Journey>()
                .With(journey => journey.Chat, chatSecond)
                .With(journey => journey.DepartureTime, new DateTime(2021, 12, 10))
                .Create();
            chatSecond.Journeys = new List<Journey>() { journeySecond };

            var chatThird = Fixture.Build<Chat>()
                .With(chat => chat.Messages, new List<Message>())
                .Create();
            var journeyThird = Fixture.Build<Journey>()
                .With(journey => journey.Chat, chatThird)
                .With(journey => journey.DepartureTime, new DateTime(2021, 12, 10))
                .Create();
            chatSecond.Journeys = new List<Journey>() { journeyThird };

            var users = Fixture.Build<User>()
                .With(u => u.Id, journeyFirst.OrganizerId)
                .CreateMany(1);
            var user = users.First();
            var mock = users
                .AsQueryable()
                .BuildMock();

            userRepository
                .Setup(rep => rep.Query())
                .Returns(mock.Object);

            var claims = new List<Claim>() { new("preferred_username", user.Email) };

            httpContextAccessor
                .Setup(h => h.HttpContext.User.Claims)
                .Returns(claims);

            user.OrganizerJourneys = new List<Journey>() { journeyFirst };
            user.ParticipantJourneys = new List<Journey>() { journeySecond, journeyThird };

            var expectedChats = Mapper.Map<IEnumerable<Chat>, IEnumerable<ChatDto>>(new List<Chat>() { chatSecond, chatFirst, chatThird });

            // Act
            var result = await chatService.GetUserChatsAsync();

            // Assert
            result.Should().Equal(expectedChats, (chatResult, expectedChat) => chatResult.Id == expectedChat.Id);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddMessageAsync_WhenMessageIsValid_ReturnsMessageObject(Message message)
        {
            // Arrange
            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(receivedMessages => receivedMessages.ChatId, message.ChatId)
                .CreateMany()
                .ToList();
            var participants = Fixture.Build<User>()
                .With(user => user.ReceivedMessages, receivedMessages)
                .CreateMany(1)
                .ToList();
            var journey = Fixture.Build<Journey>()
                .With(journey => journey.Id, message.ChatId)
                .With(journey => journey.Participants, participants)
                .Create();
            var chat = Fixture.Build<Chat>()
                .With(chat => chat.Id, message.ChatId)
                .With(chat => chat.Id, journey.ChatId)
                .CreateMany(1);

            chatRepository.Setup(repo => repo.Query())
                .Returns(chat.AsQueryable().BuildMock().Object);
            messageRepository.Setup(repo => repo.AddAsync(message)).ReturnsAsync(message);

            // Act
            var result = await chatService.AddMessageAsync(message);

            // Assert
            result.Should().BeEquivalentTo(message);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddMessageAsync_WhenMessageIsNotValid_ReturnsNull(Message message)
        {
            // Arrange
            messageRepository.Setup(repo => repo.AddAsync(message)).ReturnsAsync((Message)null);

            // Act
            var result = await chatService.AddMessageAsync(message);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task IncrementUnreadMessagesAsync_Increments_Unread_Messages_In_The_Chat(int chatId, int senderId)
        {
            // Arrange
            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.ChatId, chatId)
                .CreateMany(1)
                .ToList();

            var expected = receivedMessages
                .FirstOrDefault(rm => rm.ChatId == chatId && rm.UserId != senderId)!.UnreadMessagesCount + 1;

            receivedMessagesRepository.Setup(repo => repo.Query())
                .Returns(receivedMessages.AsQueryable().BuildMock().Object);

            // Act
            await chatService.IncrementUnreadMessagesAsync(chatId, senderId);
            var result = receivedMessages
                .FirstOrDefault(rm => rm.ChatId == chatId && rm.UserId != senderId)!.UnreadMessagesCount;

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetChatByIdAsync_ChatExist_ReturnsChat(Chat chat)
        {
            // Arrange
            chatRepository.Setup(repo => repo.GetByIdAsync(chat.Id)).ReturnsAsync(chat);

            // Act
            var result = await chatService.GetChatByIdAsync(chat.Id);

            // Assert
            result.Should().Be(chat);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetChatByIdAsync_IdNotMatchesChat_ReturnsNull(Chat chat)
        {
            // Arrange
            chatRepository.Setup(repo => repo.GetByIdAsync(chat.Id)).ReturnsAsync(chat);

            // Act
            var result = await chatService.GetChatByIdAsync(chat.Id + 1);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [InlineData("test message", "test")]
        [InlineData("we@ther ewq", "ewq")]
        public async Task GetFilteredChatsAsync_MessagesContainSearchText_ReturnsChats(string message, string searchText)
        {
            // Arrange
            GetInitializedChatsData(out var messages, out var filter, out var expectedResult);

            var messageList = messages.ToList();
            messageList.First().Text = message;
            filter.SearchText = searchText;
            var expectedChat = expectedResult.ToList();
            expectedChat.First().MessageText = message;

            messageRepository.Setup(repo => repo.Query())
                .Returns(messageList.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetFilteredChatsAsync(filter);

            // Assert
            result.Should().BeEquivalentTo(expectedChat);
        }

        [Theory]
        [InlineData("test message", "msg")]
        [InlineData("we@ther", "ath")]
        public async Task GetFilteredChatsAsync_MessagesNotContainSearchText_ReturnsEmptyCollection(string message, string searchText)
        {
            // Arrange
            GetInitializedChatsData(out var messages, out var filter, out var expectedResult);

            var messageList = messages.ToList();
            messageList.First().Text = message;
            filter.SearchText = searchText;
            expectedResult = new List<ChatDto>();

            messageRepository.Setup(repo => repo.Query())
                .Returns(messageList.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetFilteredChatsAsync(filter);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        private void GetInitializedChatsData(out IEnumerable<Message> messages, out ChatFilter filter, out IEnumerable<ChatDto> expectedResult)
        {
            var chat = Fixture.Create<Chat>();
            var chatDto = Mapper.Map<ChatDto>(chat);
            var chatDtoList = new List<ChatDto>()
            {
                chatDto,
            };

            messages = Fixture.Build<Message>()
            .With(x => x.ChatId, chat.Id)
            .With(x => x.Chat, chat)
            .CreateMany()
            .ToList();

            filter = Fixture.Build<ChatFilter>()
            .With(x => x.SearchText, messages.First().Text)
            .With(x => x.Chats, chatDtoList)
            .Create();

            expectedResult = chatDtoList;
            expectedResult.First().MessageText = messages.First().Text;
            expectedResult.First().MessageId = messages.First().Id;
        }
    }
}
