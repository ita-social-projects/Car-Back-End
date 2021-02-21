using System.Collections.Generic;
using System.Linq;
using Car.Data.FluentValidation;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Extensions;
using Car.Domain.Services.Interfaces;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork<CarEntity> carUnitOfWork;

        public CarService(IUnitOfWork<CarEntity> carUnitOfWork) =>
            this.carUnitOfWork = carUnitOfWork;

        public CarEntity AddCar(CarEntity car)
        {
            var newCar = carUnitOfWork.GetRepository().Add(car);
            carUnitOfWork.SaveChanges();

            return newCar;
        }

        public CarDto GetCarById(int carId)
        {
            var car = carUnitOfWork.GetRepository().Query()
                .IncludeModelWithBrand()
                .Select(c => new CarDto
                {
                    Id = c.Id,
                    Model = c.Model,
                    Color = c.Color,
                    PlateNumber = c.PlateNumber,
                    ImageId = c.ImageId,
                    OwnerId = c.OwnerId,
                })
                .FirstOrDefault(c => c.Id == carId);

            return car;
        }

        public IEnumerable<CarDto> GetAllByUserId(int userId)
        {
            var cars = carUnitOfWork.GetRepository()
                .Query()
                .IncludeModelWithBrand()
                .Where(car => car.OwnerId == userId)
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    Model = car.Model,
                    Color = car.Color,
                    PlateNumber = car.PlateNumber,
                    ImageId = car.ImageId,
                    OwnerId = car.OwnerId,
                });

            return cars;
        }

        public CarEntity UpdateCar(CarEntity car) =>
            carUnitOfWork.GetRepository().Update(car);
    }
}
