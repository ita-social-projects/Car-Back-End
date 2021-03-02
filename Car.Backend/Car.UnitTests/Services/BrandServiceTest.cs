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
        private readonly Mock<IRepository<Brand>> brandRepository;

        public BrandServiceTest()
        {
            brandRepository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(brandRepository.Object);
        }

        [Fact]
        public async Task GetAllBrands_WhenBrandsExist_ReturnsBrandCollection()
        {
            // Arrange
            var brands = Fixture.Create<List<Brand>>();

            brandRepository.Setup(r => r.Query())
                .Returns(brands.AsQueryable().BuildMock().Object);

            // Act
            var result = await brandService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(brands);
        }

        [Fact]
        public async Task GetAllBrands_WhenBrandsNotExist_ReturnsEmptyCollection()
        {
            // Arrange
            var brands = new List<Brand>();

            brandRepository.Setup(r => r.Query())
                .Returns(brands.AsQueryable().BuildMock().Object);

            // Act
            var result = await brandService.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
