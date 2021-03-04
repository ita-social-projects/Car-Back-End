﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
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
    public class NotificationControllerTest : TestBase
    {
        private readonly Mock<INotificationService> notificationService;
        private readonly NotificationController notificationController;
        private readonly Mock<IHubContext<SignalRHub>> hubContext;

        public NotificationControllerTest()
        {
            notificationService = new Mock<INotificationService>();
            hubContext = new Mock<IHubContext<SignalRHub>>();
            notificationController = new NotificationController(notificationService.Object, hubContext.Object);
        }

        [Fact]
        public async Task GetNotificationAsync_WhenNotificationExists_ReturnsOkObjectResult()
        {
            // Arrange
            var notification = Fixture.Create<Notification>();

            notificationService.Setup(service => service.GetNotificationAsync(notification.Id)).ReturnsAsync(notification);

            // Act
            var result = await notificationController.GetNotificationAsync(notification.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(notification);
            }
        }

        [Fact]
        public async Task GetNotificationsAsync_WhenNotificationsExist_ReturnsOkObjectResult()
        {
            // Arrange
            var notifications = Fixture.Create<List<Notification>>();
            var user = Fixture.Create<User>();

            notificationService.Setup(service => service.GetNotificationsAsync(user.Id)).ReturnsAsync(notifications);

            // Act
            var result = await notificationController.GetNotificationsAsync(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(notifications);
            }
        }
    }
}