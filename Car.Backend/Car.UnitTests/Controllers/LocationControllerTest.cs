using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
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
    public class LocationControllerTest : TestBase
    {
        private readonly Mock<ILocationService> locationService;
        private readonly LocationController locationController;

        public LocationControllerTest()
        {
            locationService = new Mock<ILocationService>();
            locationController = new LocationController(locationService.Object);
        }

        [Fact]
        public async Task GetAllByUserId_WhenLocationsExist_ReturnsOkObjectResult()
        {
            // Arrange
            var locations = Fixture.CreateMany<Location>();
            var user = Fixture.Create<User>();

            locationService.Setup(service => service.GetAllByUserIdAsync(user.Id)).ReturnsAsync(locations);

            // Act
            var result = await locationController.GetAllByUserId(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(locations);
            }
        }

        [Fact]
        public async Task AddLocation_WhenLocationIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var locationDto = Fixture.Create<LocationDTO>();

            var expectedLocation = Mapper.Map<LocationDTO, Location>(locationDto);

            locationService.Setup(service => service.AddLocationAsync(locationDto)).ReturnsAsync(expectedLocation);

            // Act
            var result = await locationController.Add(locationDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedLocation);
            }
        }

        [Fact]
        public async Task GetLocationById_WhenLocationExists_ReturnsOkObjectResult()
        {
            // Arrange
            var location = Fixture.Create<Location>();

            locationService.Setup(service => service.GetLocationByIdAsync(location.Id)).ReturnsAsync(location);

            // Act
            var result = await locationController.Get(location.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(location);
            }
        }

        [Fact]
        public async Task DeleteAsync_WhenLocationExists_ReturnsOkResult()
        {
            // Arrange
            var location = Fixture.Create<Location>();

            // Act
            var result = await locationController.DeleteAsync(location.Id);

            // Assert
            locationService.Verify(service => service.DeleteAsync(location.Id), Times.Once());
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteAsync_WhenLocationNotExists_ThrowDbUpdateConcurrencyException()
        {
            // Arrange
            var location = Fixture.Create<Location>();
            locationService.Setup(service => service.DeleteAsync(location.Id)).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = locationService.Invoking(service => service.Object.DeleteAsync(location.Id));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }
    }
}
