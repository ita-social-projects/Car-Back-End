using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Models.Car;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CarEntity = Car.Data.Entities.Car;

namespace Car.UnitTests.Controllers
{
    public class CarControllerTest : TestBase
    {
        private readonly Mock<ICarService> carService;
        private readonly CarController carController;

        public CarControllerTest()
        {
            carService = new Mock<ICarService>();
            carController = new CarController(carService.Object);
        }

        [Fact]
        public async Task GetAllByUserId_WhenCarsExist_ReturnsOkObjectResult()
        {
            // Arrange
            var cars = Fixture.CreateMany<CarEntity>();
            var user = Fixture.Create<User>();

            carService.Setup(service => service.GetAllByUserIdAsync(user.Id)).ReturnsAsync(cars);

            // Act
            var result = await carController.GetAllByUserId(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(cars);
            }
        }

        [Fact]
        public async Task AddCar_WhenCarIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var createCarModel = Fixture.Create<CreateCarModel>();
            var expectedCar = Mapper.Map<CreateCarModel, CarEntity>(createCarModel);

            carService.Setup(service => service.AddCarAsync(createCarModel)).ReturnsAsync(expectedCar);

            // Act
            var result = await carController.AddCar(createCarModel);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedCar);
            }
        }

        [Fact]
        public async Task GetCarById_WhenCarExists_ReturnsOkObjectResult()
        {
            // Arrange
            var car = Fixture.Create<CarEntity>();

            carService.Setup(service => service.GetCarByIdAsync(car.Id)).ReturnsAsync(car);

            // Act
            var result = await carController.GetCarById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(car);
            }
        }

        [Fact]
        public async Task UpdateCar_WhenCarIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var updateCarModel = Fixture.Create<UpdateCarModel>();
            var expectedCar = Mapper.Map<UpdateCarModel, CarEntity>(updateCarModel);

            carService.Setup(service => service.UpdateCarAsync(updateCarModel)).ReturnsAsync(expectedCar);

            // Act
            var result = await carController.UpdateCar(updateCarModel);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedCar);
            }
        }
    }
}
