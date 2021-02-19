using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoMapper;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using CarEntity = Car.Data.Entities.Car;
using MapperProfile = Car.Domain.Mapping.Mapper;

namespace Car.UnitTests.Services
{
    public class CarServiceTest
    {
        private readonly ICarService carService;
        private readonly Mock<IRepository<CarEntity>> repository;
        private readonly Mock<IUnitOfWork<CarEntity>> unitOfWork;
        private readonly Fixture fixture;
        private readonly IMapper mapper;

        public CarServiceTest()
        {
            repository = new Mock<IRepository<CarEntity>>();
            unitOfWork = new Mock<IUnitOfWork<CarEntity>>();

            carService = new CarService(unitOfWork.Object);

            MapperProfile profile = new MapperProfile();
            mapper = new Mapper(new MapperConfiguration(m => m.AddProfile(profile)));

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void GetCarById_CarExists_ReturnsCarObject()
        {
            // Arrange
            var cars = fixture.Create<List<CarEntity>>();
            var car = fixture.Build<CarEntity>()
                .With(c => c.Id, cars.Max(c => c.Id) + 1)
                .Create();
            cars.Add(car);

            repository.Setup(r => r.Query())
                .Returns(cars.AsQueryable());
            unitOfWork.Setup(u => u.GetRepository())
                .Returns(repository.Object);

            // Act
            var result = carService.GetCarById(car.Id);

            // Assert
            result.Should().BeEquivalentTo(mapper.Map<CarEntity, CarDto>(car));
        }

        [Fact]
        public void GetCarById_CarNotExist_ReturnsNull()
        {
            // Arrange
            var cars = fixture.CreateMany<CarEntity>();

            repository.Setup(r => r.Query())
               .Returns(cars.AsQueryable);
            unitOfWork.Setup(r => r.GetRepository())
               .Returns(repository.Object);

            // Act
            var result = carService.GetCarById(cars.Max(car => car.Id) + 1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void AddCar_CarAdded_ReturnsAddedCar()
        {
            // Arrange
            var car = fixture.Create<CarEntity>();

            repository.Setup(r => r.Add(car))
                .Returns(car);
            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            // Act
            var result = carService.AddCar(car);

            // Assert
            result.Should().BeEquivalentTo(car);
        }

        [Fact]
        public void AddCar_CarNotAdded_ReturnsNull()
        {
            // Arrange
            var car = fixture.Create<CarEntity>();

            repository.Setup(r => r.Add(car))
                .Returns(car);
            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            // Act
            var result = carService.AddCar(car);

            // Assert
            result.Should().BeEquivalentTo(car);
        }
    }
}
