using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Models.Location;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
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
            var createLocationModel = Fixture.Create<CreateLocationModel>();

            var expectedLocation = Mapper.Map<CreateLocationModel, Location>(createLocationModel);

            locationService.Setup(service => service.AddLocationAsync(createLocationModel)).ReturnsAsync(expectedLocation);

            // Act
            var result = await locationController.Add(createLocationModel);

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
    }
}
