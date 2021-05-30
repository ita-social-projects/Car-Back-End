using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Constants;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;
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
    public class RequestControllerTest : TestBase
    {
        private readonly Mock<IRequestService> requestService;
        private readonly RequestController requestController;

        public RequestControllerTest()
        {
            requestService = new Mock<IRequestService>();
            requestController = new RequestController(requestService.Object);
        }

        [Fact]
        public async Task Get_WhenRequestExists_ReturnsRequestObject()
        {
            // Arrange
            var requestEntity = Fixture.Create<Request>();
            var expectedResult = Mapper.Map<Request, RequestDto>(requestEntity);

            requestService.Setup(r => r.GetRequestByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult);

            // Act
            var result = await requestController.Get(requestEntity.Id);

            // Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
            (result as OkObjectResult)?.Value.Should().Be(expectedResult);
        }

        [Fact]
        public async Task Get_WhenRequestDoesntExist_ReturnsNull()
        {
            // Arrange
            var requestEntity = Fixture.Create<Request>();
            var expectedResult = Mapper.Map<Request, RequestDto>(requestEntity);

            requestService.Setup(r => r.GetRequestByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((RequestDto)null);

            // Act
            var result = await requestController.Get(requestEntity.Id);

            // Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
            (result as OkObjectResult)?.Value.Should().BeNull();
        }

        [Fact]
        public async Task Add_WhenRequestIsValid_ReturnsAddedRequest()
        {
            // Arrange
            var expectedResult = Fixture.Create<RequestDto>();

            requestService.Setup(r => r.AddRequestAsync(It.IsAny<RequestDto>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await requestController.Add(expectedResult);

            // Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
            (result as OkObjectResult)?.Value.Should().Be(expectedResult);
        }

        [Fact]
        public async Task Delete_WhenRequestExists_ReturnsOkObjectResult()
        {
            // Arrange
            var request = Fixture.Create<Request>();

            requestService.Setup(r => r.DeleteAsync(It.IsAny<int>()));

            // Act
            var result = await requestController.Delete(request.Id);

            // Assert
            requestService.Verify(r => r.DeleteAsync(request.Id), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Delete_WhenRequestDoesntExist_ThrowsDbUpdateConcurrencyException()
        {
            // Arrange
            var request = Fixture.Create<Request>();
            var requestIdToDelete = request.Id;

            requestService.Setup(r => r.DeleteAsync(requestIdToDelete))
                .Throws<DbUpdateConcurrencyException>();

            // Act
            var result = requestService.Invoking(r => r.Object.DeleteAsync(requestIdToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task GetByUserId_WhenRequestsExist_ReturnsRequestsCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var requests = Fixture.Create<List<RequestDto>>();

            requestService.Setup(r => r.GetRequestsByUserIdAsync(user.Id))
                .ReturnsAsync(requests);

            // Act
            var result = await requestController.GetByUserId(user.Id);

            // Assert
            (result as OkObjectResult)?.StatusCode.Should().Be(Constants.OkStatusCode);
            (result as OkObjectResult)?.Value.Should().Be(requests);
        }

        [Fact]
        public async Task GetRequestsByUserIdAsync_WhenRequestDoesntExist_ReturnsEmptyCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var requests = Fixture.Create<IEnumerable<RequestDto>>();

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
