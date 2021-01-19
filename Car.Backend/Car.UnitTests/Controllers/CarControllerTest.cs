using System.Threading.Tasks;
using Car.Controllers;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class CarControllerTest
    {
        private readonly Mock<ICarService> carService;
        private readonly Mock<IImageService<Data.Entities.Car, File>> imageService;
        private readonly CarController carController;

        private static Data.Entities.Car GetTestCar() =>
            new Data.Entities.Car()
            {
                Id = It.IsAny<int>(),
                Model = It.IsAny<string>(),
            };

        public CarControllerTest()
        {
            carService = new Mock<ICarService>();
            imageService = new Mock<IImageService<Data.Entities.Car, File>>();
            carController = new CarController(carService.Object, imageService.Object);
        }

        [Fact]
        public void GetCarById_WithExistsCar_ReturnCarObject()
        {
            // Arrange
            var car = GetTestCar();
            carService.Setup(c => c.GetCarById(It.IsAny<int>())).Returns(new Data.Entities.Car());

            // Act
            var result = carController.GetCarById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Data.Entities.Car>();
                ((result as OkObjectResult)?.Value as Data.Entities.Car)?.Model.Should().Be(car.Model);
            }
        }

        [Fact]
        public void GetCarById_WithExistsCar_ReturnNull()
        {
            // Arrange
            var car = GetTestCar();
            carService.Setup(c => c.GetCarById(It.IsAny<int>())).Returns((Data.Entities.Car)null);

            // Act
            var result = carController.GetCarById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task UploadCarPhoto_WithExistsCar_ReturnsCarObject()
        {
            // Arrange
            var car = GetTestCar();
            var formFile = new FormImage();
            imageService.Setup(s => s.UploadImage(It.IsAny<int>(), formFile.Image)).Returns(Task.FromResult(new Data.Entities.Car()));

            // Act
            var result = await carController.UploadCarPhoto(car.Id, formFile);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Data.Entities.Car>();
                ((result as OkObjectResult)?.Value as Data.Entities.Car)?.Model.Should().Be(car.Model);
            }
        }

        [Fact]
        public async Task UploadCarPhoto_WithExistsCar_ReturnsNull()
        {
            // Arrange
            var car = GetTestCar();
            var formFile = new FormImage();
            imageService.Setup(s => s.UploadImage(car.Id, formFile.Image)).Returns(Task.FromResult((Data.Entities.Car)null));

            // Act
            var result = await carController.UploadCarPhoto(car.Id, formFile);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task DeleteCarPhoto_WithExistsCar_ReturnsDeletedCarObject()
        {
            // Arrange
            var car = GetTestCar();
            imageService.Setup(s => s.DeleteImage(It.IsAny<int>())).Returns(Task.FromResult(new Data.Entities.Car()));

            // Act
            var result = await carController.DeleteCarPhoto(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Data.Entities.Car>();
                ((result as OkObjectResult)?.Value as Data.Entities.Car)?.Model.Should().Be(car.Model);
            }
        }

        [Fact]
        public async Task DeleteCarPhoto_WithExistsCar_ReturnsNull()
        {
            // Arrange
            var car = GetTestCar();
            imageService.Setup(s => s.DeleteImage(It.IsAny<int>())).Returns(Task.FromResult((Data.Entities.Car)null));

            // Act
            var result = await carController.DeleteCarPhoto(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetCarFileById_WithExistsCar_ReturnsStringType()
        {
            // Arrange
            var car = GetTestCar();
            imageService.Setup(s => s.GetImageBytesById(It.IsAny<int>())).Returns(Task.FromResult(string.Empty));

            // Act
            var result = await carController.GetCarFileById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<string>();
            }
        }

        [Fact]
        public async Task GetCarFileById_WithExistsCar_ReturnNull()
        {
            // Arrange
            var car = GetTestCar();
            imageService.Setup(s => s.GetImageBytesById(It.IsAny<int>())).Returns(Task.FromResult((string)null));

            // Act
            var result = await carController.GetCarFileById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }
    }
}
