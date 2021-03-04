using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Dto;
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
    public class LocationTypeControllerTest : TestBase
    {
        private readonly Mock<ILocationTypeService> locationTypeService;
        private readonly LocationTypeController locationTypeController;

        public LocationTypeControllerTest()
        {
            locationTypeService = new Mock<ILocationTypeService>();
            locationTypeController = new LocationTypeController(locationTypeService.Object);
        }

        [Fact]
        public async Task GetAll_WhenLocationTypesExist_ReturnsOkObjectResult()
        {
            // Arrange
            var locationTypes = Fixture.CreateMany<LocationType>();

            locationTypeService.Setup(service => service.GetAllLocationTypesAsync()).ReturnsAsync(locationTypes);

            // Act
            var result = await locationTypeController.GetAll();

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(locationTypes);
            }
        }
    }
}
