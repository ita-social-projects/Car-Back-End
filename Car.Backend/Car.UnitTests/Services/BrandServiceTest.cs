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
    public class BrandServiceTest
    {
        private readonly IBrandService brandService;
        private readonly Mock<IRepository<Brand>> repository;

        private readonly Fixture fixture;

        public BrandServiceTest()
        {
            repository = new Mock<IRepository<Brand>>();

            brandService = new BrandService(repository.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllBrands_WhenBrandsExist_ReturnsBrandCollection()
        {
            // Arrange
            var brands = fixture.Create<List<Brand>>();

            repository.Setup(r => r.Query())
                .Returns(brands.AsQueryable);

            // Act
            var result = await brandService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(brands);
        }
    }
}
