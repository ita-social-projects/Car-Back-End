using System.Collections.Generic;
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
    public class ModelControllerTest
    {
        private readonly Mock<IModelService> modelService;
        private readonly ModelController modelController;
        private readonly Fixture fixture;

        public ModelControllerTest()
        {
            modelService = new Mock<IModelService>();
            modelController = new ModelController(modelService.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetBrands()
        {
            var models = fixture.Create<Model[]>();

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