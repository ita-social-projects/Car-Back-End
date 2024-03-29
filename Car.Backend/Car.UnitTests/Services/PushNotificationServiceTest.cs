﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FirebaseAdmin.Messaging;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Car.UnitTests.Services
{
    public class PushNotificationServiceTest : TestBase
    {
        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendNotificationAsync_WhenNotificationSenderNotExist_SendMessageNever(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            [Frozen] Mock<IUserService> userService,
            NotificationDto notification)
        {
            // Arrange
            notification.Sender = null;
            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object, userService.Object);

            // Act
            await pushNotificationService.SendNotificationAsync(notification);

            // Assert
            firebaseService.Verify(f => f.SendAsync(It.IsAny<MulticastMessage>()), Times.Never);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendNotificationAsync_WhenChatJourneysNotExist_SendMessageNever(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            [Frozen] Mock<IUserService> userService,
            Data.Entities.Message message)
        {
            // Arrange
            var chat = Fixture.Build<Chat>()
                .With(chat => chat.Journeys, new List<Journey>())
                .With(chat => chat.Id, message.ChatId)
                .CreateMany(1);

            chatRepository.Setup(repo => repo.Query())
                .Returns(chat.AsQueryable().BuildMock().Object);

            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object, userService.Object);

            // Act
            await pushNotificationService.SendNotificationAsync(message);

            // Assert
            firebaseService.Verify(f => f.SendAsync(It.IsAny<MulticastMessage>()), Times.Never);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendNotificationAsync_WhenChatJourneysExist_SendAsyncIsCalled(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            [Frozen] Mock<IUserService> userService,
            Data.Entities.Message message)
        {
            // Arrange
            var fcmToken = Fixture.Build<FcmToken>()
                .Create();
            var participant = Fixture.Build<User>()
                .With(participant => participant.FCMTokens, new List<FcmToken>() { fcmToken })
                .Create();
            var journey = Fixture.Build<Journey>()
                .With(journey => journey.Participants, new List<User>() { participant })
                .Create();
            var chat = Fixture.Build<Chat>()
                .With(chat => chat.Journeys, new List<Journey>() { journey })
                .With(chat => chat.Id, message.ChatId)
                .CreateMany(1);
            message.Sender.Id = participant.Id + 1;

            chatRepository.Setup(repo => repo.Query())
                .Returns(chat.AsQueryable().BuildMock().Object);

            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object, userService.Object);

            // Act
            await pushNotificationService.SendNotificationAsync(message);

            // Assert
            firebaseService.Verify(f => f.SendAsync(It.IsAny<MulticastMessage>()), Times.AtLeastOnce);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendNotificationAsync_WhenNotificationIsValid_SendMessageOnce(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            [Frozen] Mock<IUserService> userService,
            List<User> users,
            List<bool> response,
            NotificationDto notification)
        {
            // Arrange
            notification.SenderId = users[0].Id;
            notification.ReceiverId = users[1].Id;
            firebaseService.Setup(f => f.SendAsync(It.IsAny<MulticastMessage>())).ReturnsAsync(response);
            userRepository.Setup(
                    repository => repository
                        .Query(
                            It.IsAny<
                                Expression<Func<User, object>>[]
                            >()))
                .Returns(users.AsQueryable().BuildMock().Object);
            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object, userService.Object);

            // Act
            await pushNotificationService.SendNotificationAsync(notification);

            // Assert
            firebaseService.Verify(p => p.SendAsync(It.IsAny<MulticastMessage>()), Times.AtLeastOnce);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendMessageAsync_WhenMessageChatExists_SendMessageOnce(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            [Frozen] Mock<IUserService> userService,
            List<bool> response,
            Data.Entities.Message message)
        {
            // Arrange
            firebaseService.Setup(f => f.SendAsync(It.IsAny<MulticastMessage>())).ReturnsAsync(response);
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

            var firstUserId = chat!.Journeys
                .SelectMany(j => j.Participants)
                .Union(chat.Journeys.Select(j => j.Organizer))
                .Where(u => u != null)
                .Distinct()
                .First().Id;

            var users = Fixture
                .Build<User>()
                .With(c => c.Id, firstUserId)
                .CreateMany(10)
                .ToList();

            userRepository.Setup(
                    repository => repository
                        .Query(It.IsAny<Expression<Func<User, object>>[]>()))
                .Returns(users.AsQueryable().BuildMock().Object);

            PushNotificationService pushNotificationService = new PushNotificationService(
                chatRepository.Object, userRepository.Object, firebaseService.Object, userService.Object);

            // Act
            await pushNotificationService.SendNotificationAsync(message);

            // Assert
            firebaseService.Verify(p => p.SendAsync(It.IsAny<MulticastMessage>()), Times.AtLeastOnce);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task SendMessageAsync_WhenMessageChatNotExists_SendMessageNever(
            [Frozen] Mock<IRepository<Chat>> chatRepository,
            [Frozen] Mock<IRepository<User>> userRepository,
            [Frozen] Mock<IFirebaseService> firebaseService,
            [Frozen] Mock<IUserService> userService,
            List<Chat> chats,
            List<User> users,
            List<bool> response,
            Data.Entities.Message message)
        {
            // Arrange
            firebaseService.Setup(f => f.SendAsync(It.IsAny<MulticastMessage>())).ReturnsAsync(response);

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
                chatRepository.Object, userRepository.Object, firebaseService.Object, userService.Object);

            // Act
            await pushNotificationService.SendNotificationAsync(message);

            // Assert
            firebaseService.Verify(f => f.SendAsync(It.IsAny<MulticastMessage>()), Times.Never);
        }
    }
}