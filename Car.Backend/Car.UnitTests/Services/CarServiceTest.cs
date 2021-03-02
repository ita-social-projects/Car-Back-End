using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Models;
using Car.Domain.Models.Car;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Xunit;
using CarEntity = Car.Data.Entities.Car;

namespace Car.UnitTests.Services
{
    public class CarServiceTest : TestBase
    {
        private readonly Mock<IRepository<CarEntity>> carRepository;
        private readonly CarService carService;
        private readonly Mock<IImageService> imageService;

        public CarServiceTest()
        {
            carRepository = new Mock<IRepository<CarEntity>>();
            imageService = new Mock<IImageService>();
            carService = new CarService(carRepository.Object, imageService.Object, Mapper);
        }

        [Fact]
        public async Task AddCarAsync_WhenCarIsValid_ReturnsCarObject()
        {
            // Arrange
            var createCarModel = Fixture.Build<CreateCarModel>()
                .With(model => model.Image, (IFormFile)null)
                .Create();
            var carEntity = Mapper.Map<CreateCarModel, CarEntity>(createCarModel);

            carRepository.Setup(repo => repo.AddAsync(It.IsAny<CarEntity>()))
                .ReturnsAsync(carEntity);

            // Act
            var result = await carService.AddCarAsync(createCarModel);

            // Assert
            result.Should().BeEquivalentTo(createCarModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task AddCarAsync_WhenCarIsNotValid_ReturnsNull()
        {
            // Arrange
            var createCarModel = Fixture.Build<CreateCarModel>()
                .With(model => model.Image, (IFormFile)null)
                .Create();

            carRepository.Setup(repo => repo.AddAsync(It.IsAny<CarEntity>()))
                .ReturnsAsync((CarEntity)null);

            // Act
            var result = await carService.AddCarAsync(createCarModel);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCarByIdAsync_WhenCarExists_ReturnsCarObject()
        {
            // Arrange
            var car = Fixture.Create<CarEntity>();
            var cars = Fixture.Create<List<CarEntity>>();
            cars.Add(car);

            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetCarByIdAsync(car.Id);

            // Assert
            result.Should().BeEquivalentTo(car);
        }

        [Fact]
        public async Task GetCarByIdAsync_WhenCarNotExist_ReturnsNull()
        {
            // Arrange
            var cars = Fixture.Create<List<CarEntity>>();

            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetCarByIdAsync(cars.Max(c => c.Id) + 1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCarsExist_ReturnsCarsCollection()
        {
            // Arrange
            var owner = Fixture.Create<User>();
            var cars = Fixture.Create<List<CarEntity>>();
            var ownCars = Fixture.Build<CarEntity>()
                .With(c => c.OwnerId, owner.Id)
                .CreateMany();
            cars.AddRange(ownCars);

            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetAllByUserIdAsync(owner.Id);

            // Assert
            result.Should().BeEquivalentTo(ownCars);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCarsNotExist_ReturnsEmptyCollection()
        {
            // Arrange
            var owner = Fixture.Create<User>();
            var cars = Fixture.Build<CarEntity>()
                .With(c => c.OwnerId, owner.Id + 1)
                .CreateMany();

            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetAllByUserIdAsync(owner.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateCarAsync_WhenCarsIsValid_ReturnsCarObject()
        {
            // Arrange
            var updateCarModel = Fixture.Build<UpdateCarModel>()
                .With(model => model.Image, (IFormFile)null)
                .Create();
            var car = Mapper.Map<UpdateCarModel, CarEntity>(updateCarModel);

            carRepository.Setup(repo => repo.UpdateAsync(It.IsAny<CarEntity>()))
                .ReturnsAsync(car);

            // Act
            var result = await carService.UpdateCarAsync(updateCarModel);

            // Assert
            result.Should().BeEquivalentTo(car);
        }
    }
}
