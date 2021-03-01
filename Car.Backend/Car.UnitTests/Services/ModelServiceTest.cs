using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly Mock<IRepository<Model>> modelRepository;
        private readonly Fixture fixture;

        public ModelServiceTest()
        {
            modelRepository = new Mock<IRepository<Model>>();

            modelService = new ModelService(modelRepository.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetModelsByBrandIdAsync_WhenModelsExist_ReturnsModelCollection()
        {
            // Arrange
            var models = fixture.Create<List<Model>>();
            var brand = fixture.Build<Brand>()
                .With(b => b.Id, models.Max(m => m.BrandId) + 1)
                .Create();
            var specificModels = fixture.Build<Model>()
                .With(model => model.BrandId, brand.Id)
                .CreateMany();

            modelRepository.Setup(r => r.Query())
                .Returns(models.AsQueryable);

            // Act
            var result = await modelService.GetModelsByBrandIdAsync(brand.Id);

            // Assert
            result.Should().BeEquivalentTo(specificModels);
        }
    }
}