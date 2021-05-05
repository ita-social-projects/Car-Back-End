using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Exceptions;
using Car.Domain.Models.Car;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var createCarModel = Fixture.Build<CreateCarModel>()
                .With(u => u.Image, (IFormFile)null).Create();
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
            var updateCarModel = Fixture.Build<UpdateCarModel>()
                .With(u => u.Image, (IFormFile)null).Create();
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

        [Fact]
        public async Task DeleteAsync_WhenCarIsNotInJourney_ReturnsNoContentResult()
        {
            // Arrange
            var car = Fixture.Create<CarEntity>();

            // Act
            var result = await carController.DeleteAsync(car.Id);

            // Assert
            using (new AssertionScope())
            {
                carService.Verify(service => service.DeleteAsync(car.Id), Times.Once());
                result.Should().BeOfType<NoContentResult>();
            }
        }

        [Fact]
        public async Task DeleteAsync_WhenCarNotExists_ThrowDbUpdateConcurrencyException()
        {
            // Arrange
            var car = Fixture.Create<CarEntity>();
            carService.Setup(service => service.DeleteAsync(car.Id)).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = carService.Invoking(service => service.Object.DeleteAsync(car.Id));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task DeleteAsync_WhenCarIsInJourney_ThrowDbUpdateException()
        {
            // Arrange
            var car = Fixture.Create<CarEntity>();
            carService.Setup(service => service.DeleteAsync(car.Id)).Throws<DbUpdateException>();

            // Act
            var result = carService.Invoking(service => service.Object.DeleteAsync(car.Id));

            // Assert
            await result.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
