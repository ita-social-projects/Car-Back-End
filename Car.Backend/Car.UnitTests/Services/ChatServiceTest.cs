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

        public ChatServiceTest()
        {
            chatRepository = new Mock<IRepository<Chat>>();
            userRepository = new Mock<IRepository<User>>();
            messageRepository = new Mock<IRepository<Message>>();
            receivedMessagesRepository = new Mock<IRepository<ReceivedMessages>>();
            journeyRepository = new Mock<IRepository<Journey>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            chatService = new ChatService(
                userRepository.Object,
                chatRepository.Object,
                messageRepository.Object,
                receivedMessagesRepository.Object,
                journeyRepository.Object,
                Mapper,
                httpContextAccessor.Object);
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
        public async Task AddChatAsync_WhenChatIsValid_ReturnsChatObject(CreateChatDto chat)
        {
            // Arrange
            var chatEntity = Mapper.Map<CreateChatDto, Chat>(chat);
            var journeys = Fixture.Build<Journey>()
                .With(repo => repo.Id, chat.Id)
                .CreateMany(1)
                .ToList();
            journeyRepository.Setup(repo => repo
                .Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            chatRepository.Setup(repo => repo
                .AddAsync(It.IsAny<Chat>()))
                .ReturnsAsync(chatEntity);

            // Act
            var result = await chatService.AddChatAsync(chat);

            // Assert
            result.Should().BeEquivalentTo(chatEntity);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetReceivedMessagesFromChat_ReceivedMessagesAreCorrect(Chat chat)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(repo => repo.Id, chat.Id)
                .CreateMany(1)
                .ToList();
            journeyRepository.Setup(repo => repo
                .Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetReceivedMessagesFromChat(chat.Id);

            // Assert
            result.UnreadMessagesCount.Should().Be(0);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddChatAsync_WhenChatIsNotValid_ReturnsNull(CreateChatDto chat)
        {
            // Arrange
            var chatEntity = Mapper.Map<CreateChatDto, Chat>(chat);
            chatRepository.Setup(repo => repo
                .AddAsync(chatEntity))
                .ReturnsAsync((Chat)null);

            // Act
            var result = await chatService.AddChatAsync(chat);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserChatsAsync_WhenChatsExist_ReturnsChatCollection(IEnumerable<User> users)
        {
            // Arrange
            var userList = users.ToList();
            var user = userList.First();
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.UserId, user.Id)
                .CreateMany(1);

            var chats = user.ReceivedMessages.Select(rm => rm.Chat)
                 .Except(new List<Chat>() { null! });
            var expectedChats = Mapper.Map<IEnumerable<Chat>, IEnumerable<ChatDto>>(chats);

            userRepository.Setup(repo => repo.Query())
                .Returns(userList.AsQueryable().BuildMock().Object);

            receivedMessagesRepository.Setup(rm => rm.Query())
                .Returns(receivedMessages.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetUserChatsAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedChats);
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
                .With(chat => chat.Journey, journey)
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
        [InlineData("we@ther", "@th")]
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
