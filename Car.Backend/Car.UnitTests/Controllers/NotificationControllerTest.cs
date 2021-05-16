using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Models.Notification;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using Car.WebApi.Hubs;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public async Task GetUnreadNotificationsNumberAsync_WhenNotificationsExist_ReturnsOkObjectResult()
        {
            // Arrange
            var notifications = Fixture.Create<List<Notification>>();
            var user = Fixture.Create<User>();

            notificationService.Setup(service => service.GetUnreadNotificationsNumberAsync(user.Id)).ReturnsAsync(notifications.Count);

            // Act
            var result = await notificationController.GetUnreadNotificationsNumberAsync(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(notifications.Count);
            }
        }

        [Fact]
        public async Task UpdateNotificationAsync_WhenNotificationIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var createNotificationModel = Fixture.Create<CreateNotificationModel>();
            var expectedNotification = Mapper.Map<CreateNotificationModel, Notification>(createNotificationModel);

            notificationService.Setup(service => service.CreateNewNotificationAsync(createNotificationModel))
                .ReturnsAsync(expectedNotification);
            hubContext.Setup(hub => hub.Clients.All).Returns(Mock.Of<IClientProxy>());

            // Act
            var result = await notificationController.UpdateNotificationAsync(createNotificationModel);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedNotification);
            }
        }

        [Fact]
        public async Task AddNotificationAsync_WhenNotificationIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var createNotificationModel = Fixture.Create<CreateNotificationModel>();
            var expectedNotification = Mapper.Map<CreateNotificationModel, Notification>(createNotificationModel);

            notificationService.Setup(service => service.CreateNewNotificationAsync(createNotificationModel))
                .ReturnsAsync(expectedNotification);
            hubContext.Setup(hub => hub.Clients.All).Returns(Mock.Of<IClientProxy>());

            // Act
            var result = await notificationController.AddNotificationAsync(createNotificationModel);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedNotification);
            }
        }

        [Fact]
        public async Task DeleteAsync_WhenNotificationExist_ReturnsNoContentResult()
        {
            // Arrange
            var notification = Fixture.Create<Notification>();

            // Act
            var result = await notificationController.DeleteAsync(notification.Id);

            // Assert
            notificationService.Verify(service => service.DeleteAsync(notification.Id), Times.Once());
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteAsync_WhenNotificationNotExist_ThrowDbUpdateConcurrencyException()
        {
            // Arrange
            var notification = Fixture.Create<Notification>();
            notificationService.Setup(service => service.DeleteAsync(notification.Id)).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = notificationService.Invoking(service => service.Object.DeleteAsync(notification.Id));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }
    }
}
