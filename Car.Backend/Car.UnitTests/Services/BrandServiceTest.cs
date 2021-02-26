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
    public class BrandServiceTest
    {
        private readonly IBrandService brandService;
        private readonly Mock<IRepository<Brand>> repository;

        private readonly Fixture fixture;

        public BrandServiceTest()
        {
            repository = new Mock<IRepository<Brand>>();

            brandService = new BrandService(unitOfWork.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetAllBrands()
        {
            var brands = fixture.Create<Brand[]>();

            repository.Setup(r => r.Query())
                .Returns(brands.AsQueryable());

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            brandService.GetAllBrands().Should().BeEquivalentTo(brands);
        }
    }
}
