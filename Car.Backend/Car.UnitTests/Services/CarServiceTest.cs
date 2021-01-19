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

        public CarServiceTest()
        {
            repository = new Mock<IRepository<Data.Entities.Car>>();
            unitOfWork = new Mock<IUnitOfWork<Data.Entities.Car>>();

            carService = new CarService(unitOfWork.Object);
        }

        public Data.Entities.Car GetTestCar() =>
            new Data.Entities.Car()
            {
                Id = It.IsAny<int>(),
                Brand = It.IsAny<string>(),
                Model = It.IsAny<string>(),
                Color = It.IsAny<string>(),
                PlateNumber = It.IsAny<string>(),
            };

        [Fact]
        public void TestGetCarById_WhenCarExists()
        {
            var car = GetTestCar();

            repository.Setup(repository => repository.GetById(car.Id))
                .Returns(car);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            carService.GetCarById(car.Id).Should().BeEquivalentTo(car);
        }

        [Fact]
        public void TestGetCarById_WhenCarNotExists()
        {
            var car = GetTestCar();

            repository.Setup(repository => repository.GetById(It.IsNotIn(car.Id)))
               .Returns(car);

            unitOfWork.Setup(repository => repository.GetRepository())
               .Returns(repository.Object);

            carService.GetCarById(It.IsNotIn(car.Id)).Should().BeNull();
        }
    }
}
