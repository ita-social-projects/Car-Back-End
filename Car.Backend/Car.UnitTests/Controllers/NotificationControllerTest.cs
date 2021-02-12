﻿using System.Collections.Generic;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Controllers;
using Car.WebApi.Hubs;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class NotificationControllerTest
    {
        private readonly Mock<INotificationService> notificationService;
        private readonly Mock<IHubContext<NotificationHub>> notificationHub;
        private readonly NotificationController notificationController;
        private readonly Fixture fixture;

        public NotificationControllerTest()
        {
            notificationService = new Mock<INotificationService>();
            notificationHub = new Mock<IHubContext<NotificationHub>>();
            notificationController = new NotificationController(notificationService.Object, notificationHub.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetNotificationById_WithRightId_ReturnsOkObjectResult()
        {
            // Arrange
            var notification = fixture.Create<Notification>();
            notificationService.Setup(u => u.GetNotification(notification.Id)).Returns(notification);

            // Act
            var result = notificationController.GetNotification(notification.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<NotificationDto>();
                ((result as OkObjectResult)?.Value as NotificationDto)?.Id.Should().Be(notification.Id);
                ((result as OkObjectResult)?.Value as NotificationDto)?.UserId.Should().Be(notification.SenderId);
            }
        }

        [Fact]
        public void TestGetNotificationById_WhenNotificationNotExist_ReturnsNull()
        {
            // Arrange
            var notification = fixture.Create<Notification>();
            notificationService.Setup(u => u.GetNotification(notification.Id)).Returns((Notification)null);

            // Act
            var result = notificationController.GetNotification(notification.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<BadRequestResult>();
                (result as BadRequestResult)?.Should().As<BadRequestResult>();
            }
        }

        [Fact]
        public void GetNotifications_WhenUserNotExists_ReturnsNull()
        {
            // Arrange
            var user = fixture.Create<User>();
            notificationService.Setup(x => x.GetNotifications(It.IsAny<int>())).Returns((List<Notification>)null);

            // Act
            var result = notificationController.GetNotifications(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<List<NotificationDto>>();
        }
    }
}