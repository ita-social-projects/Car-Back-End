using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.Tests.Services
{
    public class CarServiceTest
    {
        private readonly ICarService _carService;
        private readonly Mock<IRepository<DAL.Entities.Car>> _repository;
        private readonly Mock<IUnitOfWork<DAL.Entities.Car>> _unitOfWork;

        public CarServiceTest()
        {
            _repository = new Mock<IRepository<DAL.Entities.Car>>();
            _unitOfWork = new Mock<IUnitOfWork<DAL.Entities.Car>>();

            _carService = new CarService(_unitOfWork.Object);
        }

        public DAL.Entities.Car GetTestCar() =>
            new DAL.Entities.Car()
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
               .Returns((DAL.Entities.Car)null);

            _unitOfWork.Setup(repository => repository.GetRepository())
               .Returns(_repository.Object);

            _carService.GetCarById(It.IsNotIn(car.Id)).Should().BeNull();
        }
    }
}
