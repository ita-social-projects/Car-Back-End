using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Configurations;
using Car.Domain.Dto;
using Car.Domain.Hubs;
using Car.Domain.Models.Notification;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Xunit;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace Car.UnitTests.Services
{
    public class PushNotificationServiceTest : TestBase
    {
        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendNotificationAsync_WhenNotificationSenderNotExist_ReturnsNull(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            NotificationDto notification)
        {
            // Arrange
            notification.Sender = null;
            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object);

            // Act
            var result = await pushNotificationService.SendNotificationAsync(notification);

            // Assert
            result.Should().BeNull();
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendNotificationAsync_WhenNotificationIsValid_ReturnsMessageId(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            List<User> users,
            NotificationDto notification)
        {
            // Arrange
            notification.SenderId = users[0].Id;
            notification.ReceiverId = users[1].Id;
            firebaseService.Setup(f => f.SendAsync(It.IsAny<FirebaseAdmin.Messaging.Message>())).ReturnsAsync("123");
            userRepository.Setup(
                    repository => repository
                        .Query(
                            It.IsAny<
                                Expression<Func<User, object>>[]
                            >()))
                .Returns(users.AsQueryable().BuildMock().Object);
            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object);

            // Act
            var result = await pushNotificationService.SendNotificationAsync(notification);

            // Assert
            result.Should().NotBeNull();
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendMessageAsync_WhenMessageChatExists_ReturnsTrue(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            Message message)
        {
            // Arrange
            firebaseService.Setup(f => f.SendAsync(It.IsAny<FirebaseAdmin.Messaging.Message>())).ReturnsAsync("123");
            var chats = Fixture
                .Build<Chat>()
                .With(c => c.Id, message.ChatId)
                .CreateMany(10)
                .ToList();

            chatRepository.Setup(
                    repository => repository
                        .Query(It.IsAny<Expression<Func<Chat, object>>[]>()))
                .Returns(chats.AsQueryable().BuildMock().Object);

            var chat = chats
                    .Where(c => c.Id == message.ChatId)
                    .FirstOrDefault();

            var users = Fixture
                .Build<User>()
                .With(c => c.Id, chat.Journey.OrganizerId)
                .CreateMany(10)
                .ToList();

            userRepository.Setup(
                    repository => repository
                        .Query(It.IsAny<Expression<Func<User, object>>[]>()))
                .Returns(users.AsQueryable().BuildMock().Object);

            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object);

            // Act
            var result = await pushNotificationService.SendNotificationAsync(message);

            // Assert
            result.Should().Be(true);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendMessageAsync_WhenMessageChatNotExists_ReturnsFalse(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            List<Chat> chats,
            List<User> users,
            Message message)
        {
            // Arrange
            firebaseService.Setup(f => f.SendAsync(It.IsAny<FirebaseAdmin.Messaging.Message>())).ReturnsAsync("123");

            message.Chat = null;
            chatRepository.Setup(
                    repository => repository
                        .Query(It.IsAny<Expression<Func<Chat, object>>[]>()))
                .Returns(chats.AsQueryable().BuildMock().Object);

            userRepository.Setup(
                    repository => repository
                        .Query(It.IsAny<Expression<Func<User, object>>[]>()))
                .Returns(users.AsQueryable().BuildMock().Object);

            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object);

            // Act
            var result = await pushNotificationService.SendNotificationAsync(message);

            // Assert
            result.Should().Be(false);
        }
    }
}