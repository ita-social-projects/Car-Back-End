using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Car.Data.Constants;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class RequestControllerTest : TestBase
    {
        private readonly Mock<IRequestService> requestService;
        private readonly RequestController requestController;

        public RequestControllerTest()
        {
            requestService = new Mock<IRequestService>();
            requestController = new RequestController(requestService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task Get_WhenRequestExists_ReturnsRequestObject(Request expectedResult)
        {
            // Arrange
            requestService.Setup(r => r.GetRequestByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult);

            // Act
            var result = await requestController.Get(expectedResult.Id);

            // Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
            (result as OkObjectResult)?.Value.Should().Be(expectedResult);
        }

        [Theory]
        [AutoEntityData]
        public async Task Get_WhenRequestDoesntExist_ReturnsNull(Request requestEntity)
        {
            // Arrange
            var expectedResult = Mapper.Map<Request, RequestDto>(requestEntity);

            requestService.Setup(r => r.GetRequestByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Request)null);

            // Act
            var result = await requestController.Get(requestEntity.Id);

            // Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
            (result as OkObjectResult)?.Value.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task Add_WhenRequestIsValid_ReturnsAddedRequest(RequestDto expectedResult)
        {
            // Arrange
            requestService.Setup(r => r.AddRequestAsync(It.IsAny<RequestDto>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await requestController.Add(expectedResult);

            // Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
            (result as OkObjectResult)?.Value.Should().Be(expectedResult);
        }

        [Theory]
        [AutoEntityData]
        public async Task Delete_WhenRequestExists_ReturnsOkObjectResult(Request request)
        {
            // Arrange
            requestService.Setup(r => r.DeleteAsync(It.IsAny<int>()));

            // Act
            var result = await requestController.Delete(request.Id);

            // Assert
            requestService.Verify(r => r.DeleteAsync(request.Id), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoData]
        public async Task Delete_WhenRequestDoesntExist_ThrowsDbUpdateConcurrencyException(int requestIdToDelete)
        {
            // Arrange
            requestService.Setup(r => r.DeleteAsync(requestIdToDelete))
                .Throws<DbUpdateConcurrencyException>();

            // Act
            var result = requestService.Invoking(r => r.Object.DeleteAsync(requestIdToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetByUserId_WhenRequestsExist_ReturnsRequestsCollection(User user, List<Request> requests)
        {
            // Arrange
            requestService.Setup(r => r.GetRequestsByUserIdAsync(user.Id))
                .ReturnsAsync(requests);

            // Act
            var result = await requestController.GetByUserId(user.Id);

            // Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
            (result as OkObjectResult)?.Value.Should().Be(requests);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRequestsByUserIdAsync_WhenRequestDoesntExist_ReturnsEmptyCollection(User user, IEnumerable<Request> requests)
        {
            // Arrange
            requestService.Setup(r => r.GetRequestsByUserIdAsync(user.Id))
                .ReturnsAsync(requests);

            // Act
            var result = await requestController.GetByUserId(user.Id);

            // Assert
            (result as OkObjectResult)?.Value.Should().BeEquivalentTo(requests);
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
        }
    }
}
