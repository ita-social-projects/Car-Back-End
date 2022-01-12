using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly Mock<IRepository<User>> userRepository;

        public RequestServiceTest()
        {
            requestRepository = new Mock<IRepository<Request>>();
            notificationService = new Mock<INotificationService>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            userRepository = new Mock<IRepository<User>>();
            requestService = new RequestService(
                notificationService.Object,
                userRepository.Object,
                requestRepository.Object,
                Mapper,
                httpContextAccessor.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddRequestAsync_WhenRequestIsValid_ReturnsRequestObject(RequestDto requestDto)
        {
            // Arrange
            var request = Mapper.Map<RequestDto, Request>(requestDto);

            requestRepository.Setup(r => r.AddAsync(It.IsAny<Request>()))
                .ReturnsAsync(request);

            // Act
            var result = await requestService.AddRequestAsync(requestDto);

            // Assert
            result.Should().BeEquivalentTo(requestDto, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task AddRequestAsync_WhenRequestIsNotValid_ReturnsNull(RequestDto requestDto)
        {
            // Arrange
            requestRepository.Setup(r => r.AddAsync(It.IsAny<Request>()))
                .ReturnsAsync((Request)null);

            // Act
            var result = await requestService.AddRequestAsync(requestDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenRequestExistsAndUserIsOwner_SaveChangesExecutesOnce(RequestDto requestDto)
        {
            // Arrange
            Request request = Mapper.Map<RequestDto, Request>(requestDto);
            requestRepository.Setup(repo => repo.GetByIdAsync(request.Id)).ReturnsAsync(request);
            var user = Fixture.Build<User>().With(u => u.Id, request.UserId).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            // Act
            await requestService.DeleteAsync(request.Id);

            // Assert
            requestRepository.Verify(r => r.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenRequestExistsAndUserIsNotOwner_SaveChangesExecutesNever(RequestDto requestDto)
        {
            // Arrange
            Request request = Mapper.Map<RequestDto, Request>(requestDto);
            requestRepository.Setup(repo => repo.GetByIdAsync(request.Id)).ReturnsAsync(request);
            var user = Fixture.Build<User>().With(u => u.Id, request.UserId + 1).Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            // Act
            await requestService.DeleteAsync(request.Id);

            // Assert
            requestRepository.Verify(r => r.SaveChangesAsync(), Times.Never());
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteOutdatedAsync_DeletesRangeOnce(List<Request> requests)
        {
            // Arrange
            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            await requestService.DeleteOutdatedAsync();

            // Assert
            requestRepository.Verify(r => r.DeleteRangeAsync(It.IsAny<List<Request>>()), Times.Once);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllAsync_ReturnsRequestCollection(List<Request> requests)
        {
            // Arrange
            var expectedRequests = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestDto>>(requests);

            requestRepository.Setup(r => r.Query()).Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedRequests);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRequestByIdAsync_WhenRequestExists_ReturnsRequestObject(IEnumerable<Request> requests)
        {
            // Arrange
            var requestId = requests.First().Id;
            var expectedRequest = Mapper.Map<Request, RequestDto>(requests.First());

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetRequestByIdAsync(requestId);

            // Assert
            result.Should().BeEquivalentTo(expectedRequest);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRequestByIdAsync_WhenRequestDoesntExist_ReturnsNull(IEnumerable<Request> requests, int requestId)
        {
            // Arranges
            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetRequestByIdAsync(requestId);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRequestsByUserIdAsync_WhenRequestExists_ReturnsRequestCollection(User user, List<Request> requests)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var expectedResult = Fixture.Build<Request>()
                .With(r => r.UserId, user.Id)
                .CreateMany();
            requests.AddRange(expectedResult);

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetRequestsByUserIdAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedResult, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRequestsByUserIdAsync_WhenRequestDoesntExist_ReturnsEmptyCollection(User user, List<Request> requests)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            // Act
            var result = await requestService.GetRequestsByUserIdAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRequestsJourneysAsync_WhenJourneysExists_ReturnsRequestCollection(User user, List<Request> requests)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var requestedJourney = Fixture.Build<Request>()
                .With(r => r.UserId, user.Id)
                .CreateMany();
            requests.AddRange(requestedJourney);

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            var expectedResult = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestDto>>(requestedJourney);
            // Act
            var result = await requestService.GetRequestsByUserIdAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedResult, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRequestsJourneysAsync_WhenJourneysDoesntExists_ReturnsRequestCollection(User user, List<Request> requests)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var requestedJourney = Fixture.Build<Request>()
                .CreateMany();
            requests.AddRange(requestedJourney);

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            var expectedResult = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestDto>>(requestedJourney);
            // Act
            var result = await requestService.GetRequestsByUserIdAsync();

            // Assert
            result.Should().NotBeEquivalentTo(expectedResult, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task NotifyUserAsync_AddsNotificationOnce(RequestDto request, Journey journey, IEnumerable<StopDto> stops)
        {
            // Arrange
            notificationService.Setup(n => n.AddNotificationAsync(It.IsAny<NotificationDto>()));

            // Act
            await requestService.NotifyUserAsync(request, journey, stops);

            // Assert
            notificationService.Verify(n => n.AddNotificationAsync(It.IsAny<NotificationDto>()), Times.AtLeastOnce);
        }
    }
}
