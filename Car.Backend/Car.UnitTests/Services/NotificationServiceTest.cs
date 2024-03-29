﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Hubs;
using Car.Domain.Models.Notification;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Xunit;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace Car.UnitTests.Services
{
    public class NotificationServiceTest : TestBase
    {
        private readonly INotificationService notificationService;
        private readonly Mock<IPushNotificationService> pushNotificationService;
        private readonly Mock<IHubContext<SignalRHub>> hubContext;
        private readonly Mock<IHubCallerClients> hubClients;
        private readonly Mock<IClientProxy> clientProxy;
        private readonly Mock<IRepository<Notification>> notificationRepository;
        private readonly Mock<IRepository<User>> userRepository;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly Mock<IJourneyUserService> journeyUserService;

        public NotificationServiceTest()
        {
            hubContext = new Mock<IHubContext<SignalRHub>>();
            notificationRepository = new Mock<IRepository<Notification>>();
            userRepository = new Mock<IRepository<User>>();
            hubClients = new Mock<IHubCallerClients>();
            clientProxy = new Mock<IClientProxy>();
            pushNotificationService = new Mock<IPushNotificationService>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            journeyUserService = new Mock<IJourneyUserService>();
            notificationService = new NotificationService(
                notificationRepository.Object,
                userRepository.Object,
                hubContext.Object,
                pushNotificationService.Object,
                Mapper,
                httpContextAccessor.Object,
                journeyUserService.Object);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task GetNotificationAsync_WhenNotificationExist_ReturnsNotification(
            List<Notification> notifications)
        {
            // Arrange
            var notification = notifications.First();

            notificationRepository.Setup(
                    repository => repository
                        .Query(
                            It.IsAny<
                                Expression<Func<Notification, object>>[]
                            >()))
                .Returns(notifications.AsQueryable().BuildMock().Object);

            // Act
            var result = await notificationService.GetNotificationAsync(notification.Id);

            // Assert
            result.Should().BeEquivalentTo(Mapper.Map<Notification, NotificationDto>(notification));
        }

        [Fact]
        public async Task GetNotificationsAsync_WhenUserExist_ReturnsNotifications()
        {
            // Arrange
            var users = Fixture.CreateMany<User>(10).ToList();
            var user = users.First();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var notifications = Fixture
                .Build<Notification>()
                .With(n => n.ReceiverId, user.Id)
                .CreateMany(10)
                .ToList();
            var expectedNotifications = notifications
                    .Where(n => n.ReceiverId == user.Id)
                    .ToList();

            notificationRepository.Setup(
                    repository => repository
                        .Query(It.IsAny<Expression<Func<Notification, object>>[]>()))
                .Returns(notifications.AsQueryable().BuildMock().Object);

            userRepository.Setup(repository => repository.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await notificationService.GetNotificationsAsync();

            // Assert
            CollectionAssert.AreEquivalent(result, expectedNotifications);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task GetUnreadNotificationsAsync_WhenUserExist_ReturnsUnreadNotificationNumber(List<User> users)
        {
            // Arrange
            var user = users.First();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var notifications = Fixture
                .Build<Notification>()
                .With(n => n.ReceiverId, user.Id)
                .With(n => n.IsRead, true)
                .CreateMany()
                .ToList();
            var expectedNotificationsNumber = notifications
                .Count(n => n.ReceiverId == user.Id && !n.IsRead);

            notificationRepository.Setup(
                    repository => repository
                        .Query(It.IsAny<Expression<Func<Notification, object>>[]>()))
                .Returns(notifications.AsQueryable().BuildMock().Object);

            userRepository.Setup(repository => repository.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await notificationService.GetUnreadNotificationsNumberAsync();

            // Assert
            result.Should().Be(expectedNotificationsNumber);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task UpdateNotificationAsync_WhenNotificationExist_ReturnsUpdatedNotification(NotificationDto notification)
        {
            // Arrange
            var notifications = Fixture
                .Build<Notification>()
                .With(n => n.Id, notification.Id)
                .CreateMany()
                .ToList();
            var updatedNotification = Mapper.Map<Notification, NotificationDto>(notifications.First());

            notificationRepository.Setup(
                    repository => repository
                        .Query(It.IsAny<Expression<Func<Notification, object>>[]>()))
                .Returns(notifications.AsQueryable().BuildMock().Object);

            // Act
            var result = await notificationService.UpdateNotificationAsync(updatedNotification);

            // Assert
            result.Should().Be(updatedNotification);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task AddNotificationAsync_WhenNotificationExist_ReturnsAddedNotification(
            Notification notification, IEnumerable<Notification> notifications, User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            notificationRepository.Setup(repo => repo.AddAsync(notification)).ReturnsAsync(notification);
            notificationRepository.Setup(repo => repo.Query()).Returns(notifications.AsQueryable().BuildMock().Object);
            hubContext.Setup(hub => hub.Clients.All).Returns(Mock.Of<IClientProxy>());
            var notificationToAdd = Mapper.Map<Notification, NotificationDto>(notification);

            // Act
            var result = await notificationService.AddNotificationAsync(notificationToAdd);

            // Assert
            result.Should().BeEquivalentTo(notificationToAdd);
        }

        [Fact]
        public async Task UpdateNotificationAsync_WhenNotExist_ReturnsNull()
        {
            // Arrange
            var notification = (Notification)null;
            var notificationToUpdate = Mapper.Map<Notification, NotificationDto>(notification);

            notificationRepository
                .Setup(repo => repo.AddAsync(notification))
                .ReturnsAsync((Notification)null);

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var result = await notificationService.UpdateNotificationAsync(notificationToUpdate);

            // Assert
            result.Should().BeNull();
        }

        [Xunit.Theory]
        [AutoData]
        public async Task DeleteAsync_WhenNotificationIsNotExist_ThrowDbUpdateConcurrencyException(CreateNotificationDto notificationDto)
        {
            // Arrange
            Notification notification = Mapper.Map<CreateNotificationDto, Notification>(notificationDto);
            notificationRepository.Setup(repo => repo.GetByIdAsync(notification.Id)).ReturnsAsync(notification);
            var user = Fixture.Build<User>().With(u => u.Id, notification.ReceiverId).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            notificationRepository.Setup(repo => repo.SaveChangesAsync()).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = notificationService.Invoking(service => service.DeleteAsync(notification.Id));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task DeleteAsync_WhenNotificationExistAndUserIsReceiver_ExecuteOnce(CreateNotificationDto notificationDto, IEnumerable<Notification> notifications)
        {
            // Arrange
            Notification notification = Mapper.Map<CreateNotificationDto, Notification>(notificationDto);
            notificationRepository.Setup(repo => repo.GetByIdAsync(notification.Id)).ReturnsAsync(notification);
            var user = Fixture.Build<User>().With(u => u.Id, notification.ReceiverId).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            notificationRepository.Setup(repo => repo.Query()).Returns(notifications.AsQueryable().BuildMock().Object);
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            hubContext.Setup(hub => hub.Clients.All).Returns(Mock.Of<IClientProxy>());

            // Act
            await notificationService.DeleteAsync(notification.Id);

            // Assert
            notificationRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Xunit.Theory]
        [AutoData]
        public async Task DeleteAsync_WhenNotificationExistAndUserIsNotReceiver_ExecuteNever(CreateNotificationDto notificationDto)
        {
            // Arrange
            Notification notification = Mapper.Map<CreateNotificationDto, Notification>(notificationDto);
            notificationRepository.Setup(repo => repo.GetByIdAsync(notification.Id)).ReturnsAsync(notification);
            var user = Fixture.Build<User>().With(u => u.Id, notification.ReceiverId + 1).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            // Act
            await notificationService.DeleteAsync(notification.Id);

            // Assert
            notificationRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never());
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task CreateNewNotificationAsync_WhenNotificationExist_ReturnsNull(List<Notification> notifications)
        {
            // Arrange
            var notification = notifications.First();
            var notificationModel = Fixture.Build<CreateNotificationDto>()
                .With(n => n.Type, notification.Type)
                .With(n => n.JsonData, notification.JsonData)
                .With(n => n.ReceiverId, notification.ReceiverId)
                .With(n => n.SenderId, notification.SenderId)
                .With(n => n.JourneyId, notification.JourneyId)
                .Create();

            notificationRepository
                .Setup(repo => repo.Query())
                .Returns(notifications
                    .AsQueryable()
                    .BuildMock()
                    .Object);

            // Act
            var result = await notificationService.CreateNewNotificationAsync(notificationModel);

            // Assert
            result
                .Should()
                .BeEquivalentTo(
                    notification,
                    options => options
                        .Excluding(o => o.Id)
                        .Excluding(o => o.Receiver)
                        .Excluding(o => o.Sender)
                        .Excluding(o => o.Journey)
                        .Excluding(o => o.CreatedAt)
                        .Excluding(o => o.IsRead));
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task MarkNotificationAsReadAsync_WhenNotificationExistAndIsAllowed_ReturnsReadNotification(List<Notification> notifications)
        {
            // Arrange
            var notification = notifications.First();
            hubContext.Setup(hub => hub.Clients.All).Returns(Mock.Of<IClientProxy>());

            var user = Fixture.Build<User>().With(u => u.Id, notification.ReceiverId).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            notificationRepository
                .Setup(repo => repo.Query())
                .Returns(notifications
                    .AsQueryable()
                    .BuildMock()
                    .Object);

            // Act
            var result = await notificationService.MarkNotificationAsReadAsync(notification.Id);

            // Assert
            result.UpdatedNotificationDto.IsRead.Should().BeTrue();
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task MarkNotificationAsReadAsync_WhenNotificationExistAndIsNotAllowed_ReturnsReadNotification(List<Notification> notifications)
        {
            // Arrange
            var notification = notifications.First();
            hubContext.Setup(hub => hub.Clients.All).Returns(Mock.Of<IClientProxy>());

            var user = Fixture.Build<User>().With(u => u.Id, notification.ReceiverId + 1).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            notificationRepository
                .Setup(repo => repo.Query())
                .Returns(notifications
                    .AsQueryable()
                    .BuildMock()
                    .Object);

            // Act
            var result = await notificationService.MarkNotificationAsReadAsync(notification.Id);

            // Assert
            result.IsUpdated.Should().BeFalse();
        }

        [Fact]
        public async Task NotifyParticipantsAboutCancellationAsync_WhenJourneyHasNotParticipants_ExecuteSaveChangesNever()
        {
            // Arrange
            var journey = Fixture.Build<Journey>().With(j => j.Participants, null as List<User>).Create();

            // Act
            await notificationService.NotifyParticipantsAboutCancellationAsync(journey);

            // Assert
            notificationRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Theory]
        [AutoEntityData]
        public async Task NotifyParticipantsAboutCancellationAsync_WhenJourneyHasParticipants_ExecuteSaveChangesAtLeastOnce(User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var journey = Fixture.Create<Journey>();
            var notifications = Fixture.CreateMany<Notification>();

            NotificationInitializer(journey, notifications);

            // Act
            await notificationService.NotifyParticipantsAboutCancellationAsync(journey);

            // Assert
            notificationRepository.Verify(r => r.SaveChangesAsync(), Times.AtLeastOnce);
        }

        [Theory]
        [AutoEntityData]
        public async Task JourneyUpdateNotifyUserAsync_WhenJourneyHasParticipants_ExecuteSaveChangesAtLeastOnce(User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var journey = Fixture.Create<Journey>();
            var notifications = Fixture.CreateMany<Notification>();

            NotificationInitializer(journey, notifications);

            // Act
            await notificationService.JourneyUpdateNotifyUserAsync(journey);

            // Assert
            notificationRepository.Verify(r => r.SaveChangesAsync(), Times.AtLeastOnce);
        }

        [Fact]
        public async Task JourneyUpdateNotifyUserAsync_WhenJourneyHasNotParticipants_ExecuteSaveChangesNever()
        {
            // Arrange
            var notifications = Fixture.CreateMany<Notification>();
            var journey = Fixture.Build<Journey>().With(j => j.Participants, null as List<User>).Create();

            NotificationInitializer(journey, notifications);

            // Act
            await notificationService.JourneyUpdateNotifyUserAsync(journey);

            // Assert
            notificationRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Xunit.Theory]
        [AutoEntityData]
        public async Task DeleteNotificationsAsync_WhenNotificationsNotNull_SavesChangesOnce(
            IEnumerable<Notification> notificationsToDelete)
        {
            // Arrange

            // Act
            await notificationService.DeleteNotificationsAsync(notificationsToDelete);

            // Assert
            notificationRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [AutoEntityData]
        public async Task NotifyDriverAboutParticipantWithdrawal_SaveChangesOnce(Journey journey, int participantId, User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var notifications = Fixture.CreateMany<Notification>();

            NotificationInitializer(journey, notifications);

            // Act
            await notificationService.NotifyDriverAboutParticipantWithdrawal(journey, participantId);

            // Assert
            notificationRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        private void NotificationInitializer(Journey journey, IEnumerable<Notification> notifications)
        {
            notificationRepository.Setup(repo => repo.AddAsync(It.IsAny<Notification>())).ReturnsAsync(notifications.First());

            notificationRepository.Setup(repo => repo.Query()).Returns(notifications.AsQueryable().BuildMock().Object);

            hubContext.Setup(hub => hub.Clients.All).Returns(Mock.Of<IClientProxy>());
        }
    }
}