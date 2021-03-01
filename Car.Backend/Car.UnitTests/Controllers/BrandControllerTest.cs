using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class BrandControllerTest
    {
        private readonly Mock<IBrandService> brandService;
        private readonly BrandController brandController;
        private readonly Fixture fixture;

        public BrandControllerTest()
        {
            brandService = new Mock<IBrandService>();
            brandController = new BrandController(brandService.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetBrands_BrandsExist_ReturnsBrandCollection()
        {
            var brands = fixture.Create<List<Brand>>();

            brandService.Setup(service => service.GetAllAsync()).ReturnsAsync(brands);

            var result = await brandController.GetBrands();

            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<List<Brand>>();
            }
        }
    }
}