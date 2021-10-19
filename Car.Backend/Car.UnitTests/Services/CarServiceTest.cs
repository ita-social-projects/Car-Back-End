using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.ServiceExtension;
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
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;

        public CarServiceTest()
        {
            carRepository = new Mock<IRepository<CarEntity>>();
            imageService = new Mock<IImageService>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            carService = new CarService(carRepository.Object, imageService.Object, Mapper, httpContextAccessor.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddCarAsync_WhenCarIsValid_ReturnsCarObject(User user)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
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

        [Theory]
        [AutoEntityData]
        public async Task AddCarAsync_WhenCarIsNotValid_ReturnsNull(User user)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
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
            var carToGet = Mapper.Map<CarEntity, CarDto>(car);

            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetCarByIdAsync(car.Id);

            // Assert
            result.Should().BeEquivalentTo(carToGet);
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
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var ownCars = Fixture.Build<CarEntity>()
                .With(c => c.OwnerId, owner.Id)
                .CreateMany();
            cars.AddRange(ownCars);
            var carsToGet = Mapper.Map<IEnumerable<CarEntity>, IEnumerable<CarDto>>(ownCars);

            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetAllByUserIdAsync();

            // Assert
            result.Should().BeEquivalentTo(carsToGet);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllByUserIdAsync_WhenCarsNotExist_ReturnsEmptyCollection(User owner)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var cars = Fixture.Build<CarEntity>()
                .With(c => c.OwnerId, owner.Id + 1)
                .CreateMany();

            carRepository.Setup(repo => repo.Query())
                .Returns(cars.AsQueryable().BuildMock().Object);

            // Act
            var result = await carService.GetAllByUserIdAsync();

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

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenCarExistAndUserIsOwner_ExecuteOnce(CarDto carDto)
        {
            // Arrange
            CarEntity car = Mapper.Map<CarDto, CarEntity>(carDto);
            carRepository.Setup(repo => repo.GetByIdAsync(car.Id)).ReturnsAsync(car);
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, car.OwnerId.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            // Act
            await carService.DeleteAsync(car.Id);

            // Assert
            carRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenCarExistAndUserIsNotOwner_ExecuteNever(CarDto carDto)
        {
            // Arrange
            CarEntity car = Mapper.Map<CarDto, CarEntity>(carDto);
            carRepository.Setup(repo => repo.GetByIdAsync(car.Id)).ReturnsAsync(car);
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, (car.OwnerId + 1).ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            // Act
            await carService.DeleteAsync(car.Id);

            // Assert
            carRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never());
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenCarIsInJourney_ThrowDbUpdateException(CarDto carDto)
        {
            // Arrange
            CarEntity car = Mapper.Map<CarDto, CarEntity>(carDto);
            carRepository.Setup(repo => repo.GetByIdAsync(car.Id)).ReturnsAsync(car);
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, car.OwnerId.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            carRepository.Setup(repo => repo.SaveChangesAsync()).Throws<DbUpdateException>();

            // Act
            var result = carService.Invoking(service => service.DeleteAsync(car.Id));

            // Assert
            await result.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
