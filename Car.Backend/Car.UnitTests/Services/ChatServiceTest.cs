﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Dto.ChatDto;
using Car.Domain.Filters;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
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

        public ChatServiceTest()
        {
            chatRepository = new Mock<IRepository<Chat>>();
            userRepository = new Mock<IRepository<User>>();
            messageRepository = new Mock<IRepository<Message>>();
            chatService = new ChatService(userRepository.Object, chatRepository.Object, messageRepository.Object, Mapper);
        }

        [Fact]
        public async Task GetChatByIdAsync_WhenChatExists_ReturnsCorrectType()
        {
            // Arrange
            var chats = Fixture.CreateMany<Message>();
            var chat = chats.First();

            messageRepository.Setup(repo => repo.Query(It.IsAny<Expression<Func<Message, object>>[]>()))
                .Returns(chats.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetMessagesByChatIdAsync(chat.Id, chat.Id);

            // Assert
            result.Should().BeOfType<List<MessageDto>>();
        }

        [Fact]
        public async Task GetChatByIdAsync_WhenChatNotExist_ReturnsNull()
        {
            // Arrange
            var chats = Fixture.CreateMany<Message>();
            var id = chats.Max(chat => chat.Id) + 1;

            messageRepository.Setup(repo => repo.Query(It.IsAny<Expression<Func<Message, object>>[]>()))
                .Returns(chats.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetMessagesByChatIdAsync(id, chats.First().Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task AddChatAsync_WhenChatIsValid_ReturnsChatObject()
        {
            // Arrange
            var chat = Fixture.Create<CreateChatDto>();
            var chatEntity = Mapper.Map<CreateChatDto, Chat>(chat);
            chatRepository.Setup(repo => repo.AddAsync(It.IsAny<Chat>())).ReturnsAsync(chatEntity);

            // Act
            var result = await chatService.AddChatAsync(chat);

            // Assert
            result.Should().BeEquivalentTo(chatEntity);
        }

        [Fact]
        public async Task AddChatAsync_WhenChatIsNotValid_ReturnsNull()
        {
            // Arrange
            var chat = Fixture.Create<CreateChatDto>();
            var chatEntity = Mapper.Map<CreateChatDto, Chat>(chat);
            chatRepository.Setup(repo => repo.AddAsync(chatEntity)).ReturnsAsync((Chat)null);

            // Act
            var result = await chatService.AddChatAsync(chat);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetUserChatsAsync_WhenChatsExist_ReturnsChatCollection()
        {
            // Arrange
            var users = Fixture.CreateMany<User>();
            var user = users.First();
            var organizerJourneys = Fixture.Build<Journey>()
                .With(j => j.Organizer, user)
                .With(j => j.OrganizerId, user.Id).CreateMany();
            var participantJourneys = Fixture.Build<Journey>()
                .With(j => j.Participants, new List<User>() { user }).CreateMany();
            user.OrganizerJourneys = organizerJourneys.ToList();
            user.ParticipantJourneys = participantJourneys.ToList();

            var chats = organizerJourneys.Select(j => j.Chat)
                .Union(participantJourneys.Select(j => j.Chat))
                .Except(new List<Chat>() { null });
            var expectedChats = Mapper.Map<IEnumerable<Chat>, IEnumerable<ChatDto>>(chats);
            userRepository.Setup(repo => repo.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetUserChatsAsync(user.Id);

            // Assert
            result.Should().BeEquivalentTo(expectedChats);
        }

        [Fact]
        public async Task GetUserChatsAsync_WhenChatsNotExist_ReturnsEmptyCollection()
        {
            // Arrange
            var users = Fixture.CreateMany<User>();
            var user = users.First();
            var organizerJourneys = Fixture.Build<Journey>()
                .With(j => j.Organizer, user)
                .With(j => j.OrganizerId, user.Id)
                .With(j => j.Chat, (Chat)null).CreateMany();
            var participantJourneys = Fixture.Build<Journey>()
                .With(j => j.Participants, new List<User>() { user })
                .With(j => j.Chat, (Chat)null).CreateMany();
            user.OrganizerJourneys = organizerJourneys.ToList();
            user.ParticipantJourneys = participantJourneys.ToList();

            userRepository.Setup(repo => repo.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetUserChatsAsync(user.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task AddMessageAsync_WhenMessageIsValid_ReturnsMessageObject()
        {
            // Arrange
            var message = Fixture.Create<Message>();

            messageRepository.Setup(repo => repo.AddAsync(message)).ReturnsAsync(message);

            // Act
            var result = await chatService.AddMessageAsync(message);

            // Assert
            result.Should().BeEquivalentTo(message);
        }

        [Fact]
        public async Task AddMessageAsync_WhenMessageIsNotValid_ReturnsNull()
        {
            // Arrange
            var message = Fixture.Create<Message>();

            messageRepository.Setup(repo => repo.AddAsync(message)).ReturnsAsync((Message)null);

            // Act
            var result = await chatService.AddMessageAsync(message);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetFilteredChatsAsync_WhenChatsExist_ReturnChats()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Chat, ChatDto>();
                cfg.CreateMap<Journey, ChatJourneyDto>();
                cfg.CreateMap<User, ChatUserDto>();
            });

            var message = Fixture.CreateMany<Message>();
            var chats = message.Select(msg => msg.Chat)
            .AsQueryable()
            .ProjectTo<ChatDto>(configuration)
            .AsEnumerable();

            var filter = Fixture.Build<ChatFilter>()
            .With(x => x.SearchText, message.First().Text)
            .With(x => x.Chats, chats)
            .Create();

            messageRepository.Setup(repo => repo.Query()).Returns(message.AsQueryable().BuildMock().Object);

            // Act
            var result = await chatService.GetFilteredChatsAsync(filter);

            // Assert
            result.Should().BeEquivalentTo(message);
        }
    }
}
