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
    public class BrandServiceTest : TestBase
    {
        private readonly IBrandService brandService;
        private readonly Mock<IRepository<Brand>> repository;

        public BrandServiceTest()
        {
            repository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(repository.Object);
        }

        [Fact]
        public async Task GetAllBrands_WhenBrandsExist_ReturnsBrandCollection()
        {
            // Arrange
            var brands = Fixture.Create<List<Brand>>();

            repository.Setup(r => r.Query())
                .Returns(brands.AsQueryable().BuildMock().Object);

            // Act
            var result = await brandService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(brands);
        }
    }
}
