using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
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
    public class BrandControllerTest : TestBase
    {
        private readonly Mock<IBrandService> brandService;
        private readonly BrandController brandController;

        public BrandControllerTest()
        {
            brandService = new Mock<IBrandService>();
            brandController = new BrandController(brandService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetBrands_BrandsExist_ReturnsBrandCollection(List<Brand> brands)
        {
            // Arrange
            brandService.Setup(service => service.GetAllAsync()).ReturnsAsync(brands);

            // Act
            var result = await brandController.GetBrands();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<List<Brand>>();
            }
        }
    }
}