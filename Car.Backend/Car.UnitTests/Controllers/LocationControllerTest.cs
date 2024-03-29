﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Domain.Dto.Location;
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
        public async Task GetAllByUserId_WhenLocationsExist_ReturnsOkObjectResult(IEnumerable<Location> locations)
        {
            // Arrange
            locationService.Setup(service => service.GetAllByUserIdAsync()).ReturnsAsync(locations);

            // Act
            var result = await locationController.GetAllByUserId();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(locations);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task AddLocation_WhenLocationIsValid_ReturnsOkObjectResult(CreateLocationDto locationDto)
        {
            // Arrange
            var expectedLocation = Mapper.Map<CreateLocationDto, Location>(locationDto);
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
        public async Task UpdateLocation_WhenLocationExistsAndIsAllowed_ReturnsLocation(
            UpdateLocationDto locationModel, Location location)
        {
            // Arrange
            locationService.Setup(l => l.UpdateAsync(locationModel))
                .ReturnsAsync((true, location));

            // Act
            var result = await locationController.UpdateAsync(locationModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeOfType<Location>();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateLocation_WhenLocationExistsAndIsNotAllowed_ReturnsLocation(
            UpdateLocationDto locationModel)
        {
            // Arrange
            locationService.Setup(l => l.UpdateAsync(locationModel))
                .ReturnsAsync((false, null));

            // Act
            var result = await locationController.UpdateAsync(locationModel);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateLocation_WhenLocationNotExists_ReturnsNull(UpdateLocationDto locationModel)
        {
            // Arrange
            locationService.Setup(l => l.UpdateAsync(locationModel))
                .ReturnsAsync((true, (Location)null));

            // Act
            var result = await locationController.UpdateAsync(locationModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteAsync_WhenLocationExistsAndIsAllowed_ReturnsOkResult(Location location)
        {
            // Arrange
            locationService.Setup(service => service.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await locationController.DeleteAsync(location.Id);

            // Assert
            locationService.Verify(service => service.DeleteAsync(location.Id), Times.Once());
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteAsync_WhenLocationExistsAndIsForbidden_ReturnsOkResult(Location location)
        {
            // Arrange
            locationService.Setup(service => service.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await locationController.DeleteAsync(location.Id);

            // Assert
            locationService.Verify(service => service.DeleteAsync(location.Id), Times.Once());
            result.Should().BeOfType<ForbidResult>();
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
