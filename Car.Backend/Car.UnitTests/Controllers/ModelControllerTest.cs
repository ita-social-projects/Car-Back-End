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
    public class ModelControllerTest
    {
        private readonly Mock<IModelService> modelService;
        private readonly ModelController modelController;

        public ModelControllerTest()
        {
            modelService = new Mock<IModelService>();
            modelController = new ModelController(modelService.Object);
        }

        public IEnumerable<Model> GetTestModels() =>
            new[]
            {
                new Model()
                {
                    Id = It.IsAny<int>(),
                    Name = It.IsAny<string>(),
                    BrandId = It.IsAny<int>(),
                    Brand = It.IsAny<Brand>(),
                    Car = It.IsAny<Data.Entities.Car>(),
                },
                new Model()
                {
                    Id = It.IsAny<int>(),
                    Name = It.IsAny<string>(),
                    BrandId = It.IsAny<int>(),
                    Brand = It.IsAny<Brand>(),
                    Car = It.IsAny<Data.Entities.Car>(),
                },
            };

        [Fact]
        public void TestGetBrands()
        {
            var models = GetTestModels();

            modelService.Setup(service => service.GetModelsByBrandId(It.IsAny<int>()))
                .Returns(models);

            var result = modelController.GetAll(It.IsAny<int>());

            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Model[]>();
            }
        }
    }
}