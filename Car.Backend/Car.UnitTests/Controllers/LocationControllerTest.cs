using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Domain.Dto;
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

        [Theory]
        [AutoEntityData]
        public async Task GetAllByUserId_WhenLocationsExist_ReturnsOkObjectResult(IEnumerable<Location> locations, User user)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task AddLocation_WhenLocationIsValid_ReturnsOkObjectResult(LocationDTO locationDto)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetLocationById_WhenLocationExists_ReturnsOkObjectResult(Location location)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task DeleteAsync_WhenLocationExists_ReturnsOkResult(Location location)
        {
            // Act
            var result = await locationController.DeleteAsync(location.Id);

            // Assert
            locationService.Verify(service => service.DeleteAsync(location.Id), Times.Once());
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteAsync_WhenLocationNotExists_ThrowDbUpdateConcurrencyException(
            Location location, [Frozen]Mock<ILocationService> locationService)
        {
            // Arrange
            locationService.Setup(service => service.DeleteAsync(location.Id)).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = locationService.Invoking(service => service.Object.DeleteAsync(location.Id));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }
    }
}
