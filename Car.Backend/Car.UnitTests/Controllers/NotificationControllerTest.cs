using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Domain.Models.Notification;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class NotificationControllerTest : TestBase
    {
        private readonly Mock<INotificationService> notificationService;
        private readonly NotificationController notificationController;

        public NotificationControllerTest()
        {
            notificationService = new Mock<INotificationService>();
            notificationController = new NotificationController(notificationService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetNotificationAsync_WhenNotificationExists_ReturnsOkObjectResult(Notification notification)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetNotificationsAsync_WhenNotificationsExist_ReturnsOkObjectResult(List<Notification> notifications, User user)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetUnreadNotificationsNumberAsync_WhenNotificationsExist_ReturnsOkObjectResult(List<Notification> notifications, User user)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task AddNotificationAsync_WhenNotificationIsValid_ReturnsOkObjectResult(CreateNotificationModel createNotificationModel)
        {
            // Arrange
            var expectedNotification = Mapper.Map<CreateNotificationModel, Notification>(createNotificationModel);

            notificationService.Setup(service => service.CreateNewNotificationAsync(createNotificationModel))
                .ReturnsAsync(expectedNotification);

            // Act
            var result = await notificationController.AddNotificationAsync(createNotificationModel);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedNotification);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteAsync_WhenNotificationExist_ReturnsOkResult(Notification notification)
        {
            // Act
            var result = await notificationController.DeleteAsync(notification.Id);

            // Assert
            notificationService.Verify(service => service.DeleteAsync(notification.Id), Times.Once());
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteAsync_WhenNotificationNotExist_ThrowDbUpdateConcurrencyException(
            Notification notification, [Frozen] Mock<INotificationService> notificationService)
        {
            // Arrange
            notificationService.Setup(service => service.DeleteAsync(notification.Id)).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = notificationService.Invoking(service => service.Object.DeleteAsync(notification.Id));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }
    }
}
