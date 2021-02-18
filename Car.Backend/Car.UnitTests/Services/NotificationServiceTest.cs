// using AutoFixture;
// using Car.Data.Entities;
// using Car.Data.Interfaces;
// using Car.Domain.Services.Implementation;
// using Car.Domain.Services.Interfaces;
// using FluentAssertions;
// using Moq;
// using Xunit;
//
// namespace Car.UnitTests.Services
// {
//     public class NotificationServiceTest
//     {
//         private readonly INotificationService notificationService;
//         private readonly Mock<IRepository<Notification>> repository;
//         private readonly Mock<IUnitOfWork<Notification>> unitOfWork;
//         private readonly Fixture fixture;
//
//         public NotificationServiceTest()
//         {
//             repository = new Mock<IRepository<Notification>>();
//             unitOfWork = new Mock<IUnitOfWork<Notification>>();
//
//             notificationService = new NotificationService(unitOfWork.Object);
//
//             fixture = new Fixture();
//
//             fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
//             fixture.Behaviors.Add(new OmitOnRecursionBehavior());
//         }
//
//         [Fact]
//         public void TestGetNotifications_WhenNotificationsExist()
//         {
//             var notification = fixture.Create<Notification>();
//
//             repository.Setup(r => r.GetById(notification.Id))
//                 .Returns(notification);
//
//             unitOfWork.Setup(r => r.GetRepository())
//                 .Returns(repository.Object);
//
//             notificationService.GetNotification(notification.Id).Should().NotBeEquivalentTo(notification);
//         }
//
//         [Fact]
//         public void TestUpdateNotification()
//         {
//             var notification = fixture.Create<Notification>();
//             repository.Setup(r => r.GetById(notification.Id))
//                .Returns(notification);
//
//             unitOfWork.Setup(r => r.GetRepository())
//                 .Returns(repository.Object);
//
//             notificationService.UpdateNotification(notification).Should().BeEquivalentTo(notification);
//         }
//
//         [Fact]
//         public void TestUpdateNotification_WhenNotExist()
//         {
//             var notification = fixture.Create<Notification>();
//             repository.Setup(r => r.GetById(notification.Id))
//                .Returns(notification);
//
//             unitOfWork.Setup(r => r.GetRepository())
//                 .Returns(repository.Object);
//
//             notificationService.UpdateNotification(notification).Should().NotBeNull();
//         }
//     }
// }
