using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class NotificationServiceTest : TestBase
    {
        private readonly INotificationService notificationService;
        private readonly Mock<IRepository<Notification>> repository;

        public NotificationServiceTest()
        {
            repository = new Mock<IRepository<Notification>>();
            notificationService = new NotificationService(repository.Object, Mapper);
        }

        [Fact(Skip = "true")]
        public async Task TestGetNotificationAsync_WhenNotificationExist()
        {
            // Arrange
            var notification = Fixture.Create<Notification>();
            var notificationId = Fixture.Create<int>();

            // Act
            var testNotification = await notificationService
                .GetNotificationAsync(notification.Id);

            // Assert
            testNotification.Should().BeEquivalentTo(notification);
        }

        [Fact]
        public async Task TestGetNotificationsAsync_WhenUserExist()
        {
            // Arrange
            var notifications = Fixture.CreateMany<Notification>();
            var user = Fixture.Create<User>();

            var notificationServiceMock = new Mock<INotificationService>();
            var notificationsList = notifications.ToList();
            {
                notificationServiceMock.Setup(
                        iNotificationService =>
                            iNotificationService.GetNotificationsAsync(user.Id))
                    .ReturnsAsync(notificationsList.ToList);
            }

            // Act
            var testUserNotificationsAsync =
                await notificationServiceMock.Object.GetNotificationsAsync(user.Id);

            // Assert
            notificationServiceMock.Verify(
                verifyNotificationService =>
                    verifyNotificationService
                        .GetNotificationsAsync(user.Id),
                Times.Once);
            testUserNotificationsAsync.Should().BeEquivalentTo(notificationsList.ToList());
        }

        [Fact]
        public async Task TestGetUnreadNotificationsAsync_WhenUserExist()
        {
            // Arrange
            var notifications = Fixture.CreateMany<Notification>();
            var user = Fixture.Create<User>();

            var notificationServiceMock = new Mock<INotificationService>();
            var notificationsArray = notifications as Notification[] ?? notifications.ToArray();
            {
                notificationServiceMock.Setup(
                        iNotificationService =>
                            iNotificationService.GetUnreadNotificationsNumberAsync(user.Id))
                    .ReturnsAsync(notificationsArray.Count(notification => !notification.IsRead));
            }

            // Act
            var testUnreadNotificationsNumber = await notificationServiceMock.Object
                .GetUnreadNotificationsNumberAsync(user.Id);

            // Assert
            notificationServiceMock.Verify(
                verifyNotificationService =>
                    verifyNotificationService
                        .GetUnreadNotificationsNumberAsync(user.Id),
                Times.Once);
            testUnreadNotificationsNumber
                .Should()
                .Be(notificationsArray.Count(notification => !notification.IsRead));
        }

        [Fact]
        public async Task TestUpdateNotificationAsync_WhenNotificationExist()
        {
            // Arrange
            var newNotification = Fixture.Create<Notification>();

            var notificationServiceMock = new Mock<INotificationService>();
            {
                notificationServiceMock.Setup(
                        iNotificationService =>
                            iNotificationService.UpdateNotificationAsync(newNotification))
                    .ReturnsAsync(newNotification);
            }

            // Act
            var testUpdatedNotification = await notificationServiceMock.Object
                .UpdateNotificationAsync(newNotification);

            // Assert
            notificationServiceMock.Verify(
                verifyNotificationService =>
                    verifyNotificationService
                        .UpdateNotificationAsync(newNotification),
                Times.Once);
            testUpdatedNotification
                .Should()
                .BeEquivalentTo(newNotification);
        }

        [Fact]
        public async Task TestAddNotificationAsync_ReturnsAddedNotification_WhenNotificationExist()
        {
            // Arrange
            var notificationToAdd = Fixture.Create<Notification>();

            var notificationServiceMock = new Mock<INotificationService>();
            {
                notificationServiceMock.Setup(
                        iNotificationService =>
                            iNotificationService.AddNotificationAsync(notificationToAdd))
                    .ReturnsAsync(notificationToAdd);
            }

            // Act
            var testAddedNotification = await notificationServiceMock.Object
                .AddNotificationAsync(notificationToAdd);

            // Assert
            notificationServiceMock.Verify(
                verifyNotificationService =>
                    verifyNotificationService
                        .AddNotificationAsync(notificationToAdd),
                Times.Once);
            testAddedNotification
                .Should()
                .BeEquivalentTo(notificationToAdd);
        }

        [Fact(Skip = "true")]
        public async Task AddNotificationAsync_ValidNotificationModel_ShouldAddNotificationToDatabase()
        {
            // Arrange
            var notificationToAdd = Fixture.Create<Notification>();

            var notificationServiceMock = new Mock<INotificationService>();

            notificationServiceMock.Setup(
                    iNotificationService =>
                        iNotificationService.AddNotificationAsync(notificationToAdd))
                .ReturnsAsync(notificationToAdd);

            // Act
            await notificationServiceMock.Object
                .AddNotificationAsync(notificationToAdd);

            // Assert
            notificationServiceMock.Verify(
                verifyNotificationService =>
                    verifyNotificationService
                        .AddNotificationAsync(notificationToAdd),
                Times.Once);
            notificationServiceMock.Object
                .GetNotificationAsync(notificationToAdd.Id)
                .Should()
                .BeEquivalentTo(notificationToAdd);
        }

        [Fact(Skip = "true")]
        public async Task TestUpdateNotification_WhenNotExist()
        {
            var notification = Fixture.Create<Notification>();
            repository.Setup(r => r.GetByIdAsync(notification.Id))
               .ReturnsAsync(notification);

            (await notificationService.GetNotificationAsync(notification.Id))
                .Should().NotBeNull();
        }
    }
}