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
    public class BrandServiceTest
    {
        private readonly IBrandService brandService;
        private readonly Mock<IRepository<Brand>> repository;
        private readonly Mock<IUnitOfWork<Brand>> unitOfWork;

        public BrandServiceTest()
        {
            repository = new Mock<IRepository<Brand>>();
            unitOfWork = new Mock<IUnitOfWork<Brand>>();

            brandService = new BrandService(unitOfWork.Object);
        }

        public IEnumerable<Brand> GetTestBrands() => new Brand[]
        {
           new Brand()
           {
               Id = It.IsAny<int>(),
               Name = It.IsAny<string>(),
               Models = It.IsAny<IEnumerable<Model>>(),
               Car = It.IsAny<Data.Entities.Car>(),
           },
        };

        [Fact]
        public void TestGetAllBrands()
        {
            var brands = GetTestBrands();

            repository.Setup(repository => repository.Query())
                .Returns(brands.AsQueryable());

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            brandService.GetAllBrands().Should().BeEquivalentTo(brands);
        }
    }
}
