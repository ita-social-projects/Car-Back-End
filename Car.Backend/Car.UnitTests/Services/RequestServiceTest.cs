using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class RequestServiceTest : TestBase
    {
        private readonly Mock<IRepository<Request>> requestRepository;
        private readonly Mock<INotificationService> notificationService;
        private readonly IRequestService requestService;

        public RequestServiceTest()
        {
            requestRepository = new Mock<IRepository<Request>>();
            notificationService = new Mock<INotificationService>();
            requestService = new RequestService(notificationService.Object, requestRepository.Object, Mapper);
        }

        [Fact]
        public async Task AddRequestAsync_WhenRequestIsValid_ReturnsRequestObject()
        {
            // Arrange
            var requestDto = Fixture.Create<RequestDto>();
            var request = Mapper.Map<RequestDto, Request>(requestDto);

            requestRepository.Setup(r => r.AddAsync(It.IsAny<Request>()))
                .ReturnsAsync(request);

            // Act
            var result = await requestService.AddRequestAsync(requestDto);

            // Assert
            result.Should().BeEquivalentTo(requestDto, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task AddRequestAsync_WhenRequestIsNotValid_ReturnsNull()
        {
            // Arrange
            var requestDto = Fixture.Create<RequestDto>();

            requestRepository.Setup(r => r.AddAsync(It.IsAny<Request>()))
                .ReturnsAsync((Request)null);

            // Act
            var result = await requestService.AddRequestAsync(requestDto);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestExists_SaveChangesExecutesOnce()
        {
            // Arrange
            var requestIdToDelete = Fixture.Create<int>();

            // Act
            await requestService.DeleteAsync(requestIdToDelete);

            // Assert
            requestRepository.Verify(r => r.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task DeleteOutdatedAsync_DeletesRangeOnce()
        {
            // Arrange
            var requests = Fixture.Create<List<Request>>();
            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            await requestService.DeleteOutdatedAsync();

            // Assert
            requestRepository.Verify(r => r.DeleteRangeAsync(It.IsAny<List<Request>>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenRequestDoesntExist_ThrowsDbUpdateConcurrencyException()
        {
            // Arrange
            var requestIdToDelete = Fixture.Create<int>();
            requestRepository.Setup(r => r.SaveChangesAsync()).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = requestService.Invoking(s => s.DeleteAsync(requestIdToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsRequestCollection()
        {
            // Arrange
            var requests = Fixture.Create<List<Request>>(); //new List<Request>() { new Request() };
            var expectedRequests = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestDto>>(requests);

            requestRepository.Setup(r => r.Query()).Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedRequests);
        }

        [Fact]
        public async Task GetRequestByIdAsync_WhenRequestExists_ReturnsRequestObject()
        {
            // Arrange
            var requests = Fixture.CreateMany<Request>();
            var requestId = requests.First().Id;
            var expectedRequest = Mapper.Map<Request, RequestDto>(requests.First());

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetRequestByIdAsync(requestId);

            // Assert
            result.Should().BeEquivalentTo(expectedRequest);
        }

        [Fact]
        public async Task GetRequestByIdAsync_WhenRequestDoesntExist_ReturnsNull()
        {
            // Arrange
            var requests = Fixture.CreateMany<Request>();
            var requestId = Fixture.Create<Request>().Id;

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetRequestByIdAsync(requestId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetRequestsByUserIdAsync_WhenRequestExists_ReturnsRequestCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var expectedResult = Fixture.Build<Request>()
                .With(r => r.UserId, user.Id)
                .CreateMany();
            var requests = Fixture.Create<List<Request>>();
            requests.AddRange(expectedResult);

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetRequestsByUserIdAsync(user.Id);

            // Assert
            result.Should().BeEquivalentTo(expectedResult, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetRequestsByUserIdAsync_WhenRequestDoesntExist_ReturnsEmptyCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var requests = Fixture.Create<List<Request>>();

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetRequestsByUserIdAsync(user.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task NotifyUserAsync_AddsNotificationOnce()
        {
            // Arrange
            var notification = Fixture.Create<Notification>();
            var request = Fixture.Create<RequestDto>();
            var journey = Fixture.Create<JourneyModel>();

            notificationService.Setup(n => n.AddNotificationAsync(It.IsAny<Notification>()));

            // Act
            await requestService.NotifyUserAsync(request, journey);

            // Assert
            notificationService.Verify(n => n.AddNotificationAsync(It.IsAny<Notification>()), Times.AtLeastOnce);
        }
    }
}
