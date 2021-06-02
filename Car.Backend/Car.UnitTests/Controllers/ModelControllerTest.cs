using System.Collections.Generic;
using System.Threading.Tasks;
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

        [Theory]
        [AutoEntityData]
        public async Task GetAll_WhenModelsExist_ReturnsOkObjectResult(List<Model> models, Brand brand)
        {
            // Arrange
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