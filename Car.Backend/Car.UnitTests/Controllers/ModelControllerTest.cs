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
    public class ModelControllerTest : TestBase
    {
        private readonly Mock<IModelService> modelService;
        private readonly ModelController modelController;

        public ModelControllerTest()
        {
            modelService = new Mock<IModelService>();
            modelController = new ModelController(modelService.Object);
        }

        [Fact]
        public async Task GetAll_WhenModelsExist_ReturnsOkObjectResult()
        {
            // Arrange
            var models = Fixture.Create<List<Model>>();
            var brand = Fixture.Create<Brand>();

            modelService.Setup(service => service.GetModelsByBrandIdAsync(It.IsAny<int>()))
                .ReturnsAsync(models);

            // Act
            var result = await modelController.GetAll(brand.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<List<Model>>();
            }
        }
    }
}