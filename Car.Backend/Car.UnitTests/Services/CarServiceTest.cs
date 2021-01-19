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
        private readonly ICarService _carService;
        private readonly Mock<IRepository<Data.Entities.Car>> _repository;
        private readonly Mock<IUnitOfWork<Data.Entities.Car>> _unitOfWork;

        public CarServiceTest()
        {
            _repository = new Mock<IRepository<Data.Entities.Car>>();
            _unitOfWork = new Mock<IUnitOfWork<Data.Entities.Car>>();

            _carService = new CarService(_unitOfWork.Object);
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

            _repository.Setup(repository => repository.GetById(car.Id))
                .Returns(car);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            _carService.GetCarById(car.Id).Should().BeEquivalentTo(car);
        }

        [Fact]
        public void TestGetCarById_WhenCarNotExists()
        {
            var car = GetTestCar();

            _repository.Setup(repository => repository.GetById(It.IsNotIn(car.Id)))
               .Returns(car);

            _unitOfWork.Setup(repository => repository.GetRepository())
               .Returns(_repository.Object);

            _carService.GetCarById(It.IsNotIn(car.Id)).Should().BeNull();
        }
    }
}
