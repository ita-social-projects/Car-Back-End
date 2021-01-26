using System.Collections.Generic;
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

        public BrandControllerTest()
        {
            brandService = new Mock<IBrandService>();
            brandController = new BrandController(brandService.Object);
        }

        public IEnumerable<Brand> GetTestBrands() => new Brand[]
        {
           new Brand()
           {
               Id = It.IsAny<int>(),
               Name = It.IsAny<string>(),
               Models = It.IsAny<IEnumerable<Model>>(),
               Car = It.IsAny<Data.Entities.Car>(),
           },
           new Brand()
           {
               Id = It.IsAny<int>(),
               Name = It.IsAny<string>(),
               Models = It.IsAny<IEnumerable<Model>>(),
               Car = It.IsAny<Data.Entities.Car>(),
           },
        };

        [Fact]
        public void TestGetBrands()
        {
            var brands = GetTestBrands();

            brandService.Setup(service => service.GetAllBrands()).Returns(brands);

            var result = brandController.GetBrands();

            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Brand[]>();
            }
        }
    }
}
