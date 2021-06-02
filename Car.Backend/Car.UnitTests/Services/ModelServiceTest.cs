using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class ModelServiceTest : TestBase
    {
        private readonly IModelService modelService;
        private readonly Mock<IRepository<Model>> modelRepository;

        public ModelServiceTest()
        {
            modelRepository = new Mock<IRepository<Model>>();
            modelService = new ModelService(modelRepository.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetModelsByBrandIdAsync_WhenModelsExist_ReturnsModelCollection(List<Model> models)
        {
            // Arrange
            var brand = Fixture.Build<Brand>()
                .With(b => b.Id, models.Max(m => m.BrandId) + 1)
                .Create();
            var specificModels = Fixture.Build<Model>()
                .With(model => model.BrandId, brand.Id)
                .CreateMany();
            models.AddRange(specificModels);

            modelRepository.Setup(r => r.Query())
                .Returns(models.AsQueryable().BuildMock().Object);

            // Act
            var result = await modelService.GetModelsByBrandIdAsync(brand.Id);

            // Assert
            result.Should().BeEquivalentTo(specificModels);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetModelsByBrandIdAsync_WhenModelsNotExist_ReturnsEmptyCollection(List<Model> models)
        {
            // Arrange
            var brand = Fixture.Build<Brand>()
                .With(b => b.Id, models.Max(m => m.BrandId) + 1)
                .Create();

            modelRepository.Setup(r => r.Query())
                .Returns(models.AsQueryable().BuildMock().Object);

            // Act
            var result = await modelService.GetModelsByBrandIdAsync(brand.Id);

            // Assert
            result.Should().BeEmpty();
        }
    }
}