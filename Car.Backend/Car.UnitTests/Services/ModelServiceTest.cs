using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class ModelServiceTest
    {
        private readonly IModelService modelService;
        private readonly Mock<IRepository<Model>> repository;
        private readonly Fixture fixture;

        public ModelServiceTest()
        {
            repository = new Mock<IRepository<Model>>();
            unitOfWork = new Mock<IUnitOfWork<Model>>();

            modelService = new ModelService(unitOfWork.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetModelsByBrandId()
        {
            var models = fixture.Create<Model[]>();

            repository.Setup(r => r.Query())
                .Returns(models.AsQueryable());

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            modelService.GetModelsByBrandId(models.FirstOrDefault().BrandId)
                .Should()
                .BeEquivalentTo(models.Where(m => m.BrandId == models.FirstOrDefault().BrandId));
        }
    }
}