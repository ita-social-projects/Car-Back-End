using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Car.Data.Interfaces;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class CarServiceTest
    {
        private readonly ICarService carService;
        private readonly Mock<IRepository<Data.Entities.Car>> repository;
        private readonly Mock<IUnitOfWork<Data.Entities.Car>> unitOfWork;
        private readonly Fixture fixture;

        public CarServiceTest()
        {
            repository = new Mock<IRepository<Data.Entities.Car>>();
            unitOfWork = new Mock<IUnitOfWork<Data.Entities.Car>>();

            carService = new CarService(unitOfWork.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetCarById_WhenCarExists()
        {
            var car = fixture.Create<Data.Entities.Car>();
            var cars = fixture.Create<List<Data.Entities.Car>>();
            cars.Add(car);

            repository.Setup(r => r.Query())
                .Returns(cars.AsQueryable());

            unitOfWork.Setup(u => u.GetRepository())
                .Returns(repository.Object);

            carService.GetCarById(car.Id).Should().BeEquivalentTo(car);
        }

        [Fact]
        public void TestGetCarById_WhenCarNotExists()
        {
            var car = fixture.Create<Data.Entities.Car>();

            repository.Setup(r => r.GetById(It.IsNotIn(car.Id)))
               .Returns((Data.Entities.Car)null);

            unitOfWork.Setup(r => r.GetRepository())
               .Returns(repository.Object);

            carService.GetCarById(It.IsNotIn(car.Id)).Should().BeNull();
        }
    }
}
