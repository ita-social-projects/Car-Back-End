using System.Collections.Generic;
using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
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
        private readonly Mock<IUnitOfWork<Model>> unitOfWork;

        public ModelServiceTest()
        {
            repository = new Mock<IRepository<Model>>();
            unitOfWork = new Mock<IUnitOfWork<Model>>();

            modelService = new ModelService(unitOfWork.Object);
        }

        public IEnumerable<Model> GetTestModels() => new Model[]
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
        public void TestGetModelsByBrandId()
        {
            var models = GetTestModels();

            repository.Setup(repository => repository.Query())
                .Returns(models.AsQueryable());

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            modelService.GetModelsByBrandId(It.IsAny<int>())
                .Should()
                .BeEquivalentTo(models);
        }
    }
}
