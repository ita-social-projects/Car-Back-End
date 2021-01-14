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
        private ICarService _carService;
        private Mock<IRepository<DAL.Entities.Car>> _repository;
        private Mock<IUnitOfWork<DAL.Entities.Car>> _unitOfWork;

        public CarServiceTest()
        {
            _repository = new Mock<IRepository<DAL.Entities.Car>>();
            _unitOfWork = new Mock<IUnitOfWork<DAL.Entities.Car>>();

            _carService = new CarService(_unitOfWork.Object);
        }

        public DAL.Entities.Car GetTestCar()
        {
            return new DAL.Entities.Car()
            {
                Id = 2,
                Brand = "BMW",
                Model = "A5",
                Color = "Red",
                PlateNumber = "AA-2222-BB",
            };
        }

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
        public void TestGetUserById_WhenUserNotExist()
        {
            var car = GetTestCar();

            _repository.Setup(repository => repository.GetById(car.Id))
               .Returns(car);

            _unitOfWork.Setup(repository => repository.GetRepository())
               .Returns(_repository.Object);

            _carService.GetCarById(4).Should().BeNull();
        }
    }
}
