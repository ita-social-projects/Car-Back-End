using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
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
            var createCarModel = Fixture.Build<CreateCarDto>()
                .With(model => model.Image, (IFormFile)null)
                .Create();
            var carEntity = Mapper.Map<CreateCarDto, CarEntity>(createCarModel);

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
            var createCarModel = Fixture.Build<CreateCarDto>()
                .With(model => model.Image, (IFormFile)null)
                .Create();

            carRepository.Setup(repo => repo.AddAsync(It.IsAny<CarEntity>()))
                .ReturnsAsync((CarEntity)null);

            // Act
            var result = await carService.AddCarAsync(createCarModel);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetCarByIdAsync_WhenCarExists_ReturnsCarObject(CarEntity car, List<CarEntity> cars)
        {
            // Arrange);
            cars.Add(car);

            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetCarByIdAsync(car.Id);

            // Assert
            result.Should().BeEquivalentTo(car);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetCarByIdAsync_WhenCarNotExist_ReturnsNull(List<CarEntity> cars)
        {
            // Arrange
            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetCarByIdAsync(cars.Max(c => c.Id) + 1);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllByUserIdAsync_WhenCarsExist_ReturnsCarsCollection(User owner, List<CarEntity> cars)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetAllByUserIdAsync_WhenCarsNotExist_ReturnsEmptyCollection(User owner)
        {
            // Arrange
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
        public async Task UpdateCarAsync_WhenCarIsValid_ReturnsCarObject()
        {
            // Arrange
            var updateCarModel = Fixture.Build<UpdateCarDto>()
                .With(model => model.Image, (IFormFile)null)
                .Create();
            var inputCar = Fixture.Build<CarEntity>()
                .With(c => c.Id, updateCarModel.Id)
                .Create();

            carRepository.Setup(repo => repo.GetByIdAsync(updateCarModel.Id))
                .ReturnsAsync(inputCar);

            // Act
            var result = await carService.UpdateCarAsync(updateCarModel);

            // Assert
            result.Should().BeEquivalentTo(updateCarModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task UpdateCarAsync_WhenCarIsNotExist_ReturnsNull()
        {
            // Arrange
            var updateCarModel = Fixture.Build<UpdateCarDto>()
                .With(model => model.Image, (IFormFile)null)
                .Create();

            carRepository.Setup(repo => repo.GetByIdAsync(updateCarModel.Id))
                .ReturnsAsync((CarEntity)null);

            // Act
            var result = await carService.UpdateCarAsync(updateCarModel);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenCarIsNotExist_ThrowDbUpdateConcurrencyException(int idCarToDelete)
        {
            // Arrange
            carRepository.Setup(repo => repo.SaveChangesAsync()).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = carService.Invoking(service => service.DeleteAsync(idCarToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenCarExist_ExecuteOnce(int idCarToDelete)
        {
            // Act
            await carService.DeleteAsync(idCarToDelete);

            // Assert
            carRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenCarIsInJourney_ThrowDbUpdateException(int idCarToDelete)
        {
            // Arrange
            carRepository.Setup(repo => repo.SaveChangesAsync()).Throws<DbUpdateException>();

            // Act
            var result = carService.Invoking(service => service.DeleteAsync(idCarToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
