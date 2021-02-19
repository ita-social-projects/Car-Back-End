using System;
using System.Collections.Generic;
using System.Linq;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork<CarEntity> unitOfWork;

        public CarService(
            IUnitOfWork<CarEntity> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public CarEntity AddCar(CarEntity car)
        {
            var newCar = unitOfWork.GetRepository().Add(car);
            unitOfWork.SaveChanges();

            return car;
        }

        public CarEntity GetCarById(int carId)
        {
            var car = unitOfWork.GetRepository().Query()
                .Include(c => c.Model)
                .ThenInclude(model => model.Brand)
                .FirstOrDefault(c => c.Id == carId);

            return car;
        }

        public IEnumerable<CarDto> GetAllByUserId(int userId)
        {
            return unitOfWork.GetRepository()
                .Query()
                .Include(car => car.Model)
                .ThenInclude(model => model.Brand)
                .Where(car => car.OwnerId == userId)
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    Model = car.Model,
                    Color = car.Color,
                    PlateNumber = car.PlateNumber,
                    ImageId = car.ImageId,
                });
        }
    }
}
