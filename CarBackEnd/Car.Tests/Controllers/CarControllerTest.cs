using Moq;
using Xunit;
using Car.BLL.Services.Interfaces;
using File = Google.Apis.Drive.v3.Data.File;
using CarBackEnd.Controllers;
using FluentAssertions.Execution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Car.BLL.Dto;

namespace Car.Tests.Controllers
{
    public class CarControllerTest
    {
        private Mock<ICarService> _carService;
        private Mock<IImageService<DAL.Entities.Car, File>> _imageService;
        private CarController _carController;
        private DAL.Entities.Car GetTestCar()
        {
            return new DAL.Entities.Car()
            {
                Id = It.IsAny<int>(),
                Model = It.IsAny<string>(),
            };
        }

        public CarControllerTest()
        {
            _carService = new Mock<ICarService>();
            _imageService = new Mock<IImageService<DAL.Entities.Car, File>>();
            _carController = new CarController(_carService.Object, _imageService.Object);
        }

        [Fact]
        public void GetCarById_WithExistsCar_ReturnCarObject()
        {
            // Arrange
            var car = GetTestCar();
            _carService.Setup(c => c.GetCarById(It.IsAny<int>())).Returns(new DAL.Entities.Car());

            // Act
            var result = _carController.GetCarById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeOfType<DAL.Entities.Car>();
                ((result as OkObjectResult).Value as DAL.Entities.Car).Model.Should().Be(car.Model);
            }
        }

        [Fact]
        public void GetCarById_WithExistsCar_ReturnNull()
        {
            // Arrange
            var car = GetTestCar();
            _carService.Setup(c => c.GetCarById(It.IsAny<int>())).Returns((DAL.Entities.Car)null);

            // Act
            var result = _carController.GetCarById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task UploadCarPhoto_WithExistsCar_ReturnsCarObject()
        {
            // Arrange
            var car = GetTestCar();
            var formFile = new FormImage();
            _imageService.Setup(s => s.UploadImage(It.IsAny<int>(), formFile.image)).Returns(Task.FromResult(new DAL.Entities.Car()));

            // Act
            var result = await _carController.UploadCarPhoto(car.Id, formFile);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeOfType<DAL.Entities.Car>();
                ((result as OkObjectResult).Value as DAL.Entities.Car).Model.Should().Be(car.Model);
            }
        }

        [Fact]
        public async Task UploadCarPhoto_WithExistsCar_ReturnsNull()
        {
            // Arrange
            var car = GetTestCar();
            var formFile = new FormImage();
            _imageService.Setup(s => s.UploadImage(car.Id, formFile.image)).Returns(Task.FromResult((DAL.Entities.Car)null));

            // Act
            var result = await _carController.UploadCarPhoto(car.Id, formFile);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task DeleteCarPhoto_WithExistsCar_ReturnsDeletedCarObject()
        {
            // Arrange
            var car = GetTestCar();
            _imageService.Setup(s => s.DeleteImage(It.IsAny<int>())).Returns(Task.FromResult(new DAL.Entities.Car()));

            // Act
            var result = await _carController.DeleteCarPhoto(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeOfType<DAL.Entities.Car>();
                ((result as OkObjectResult).Value as DAL.Entities.Car).Model.Should().Be(car.Model);
            }
        }

        [Fact]
        public async Task DeleteCarPhoto_WithExistsCar_ReturnsNull()
        {
            // Arrange
            var car = GetTestCar();
            _imageService.Setup(s => s.DeleteImage(It.IsAny<int>())).Returns(Task.FromResult((DAL.Entities.Car)null));

            // Act
            var result = await _carController.DeleteCarPhoto(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetCarFileById_WithExistsCar_ReturnsStringType()
        {
            // Arrange
            var car = GetTestCar();
            _imageService.Setup(s => s.GetImageBytesById(It.IsAny<int>())).Returns(Task.FromResult(string.Empty));

            // Act
            var result = await _carController.GetCarFileById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeOfType<string>();
            }
        }

        [Fact]
        public async Task GetCarFileById_WithExistsCar_ReturnNull()
        {
            // Arrange
            var car = GetTestCar();
            _imageService.Setup(s => s.GetImageBytesById(It.IsAny<int>())).Returns(Task.FromResult((string)null));

            // Act
            var result = await _carController.GetCarFileById(car.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeNull();
            }
        }
    }
}
