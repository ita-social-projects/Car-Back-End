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

        public CarEntity AddCar(CarDto carDto)
        {
            var newCar = new CarEntity
            {
                ModelId = carDto.ModelId,
                Color = carDto.Color,
                PlateNumber = carDto.PlateNumber,
                OwnerId = carDto.OwnerId,
            };

            var car = unitOfWork.GetRepository().Add(newCar);
            unitOfWork.SaveChanges();

            return car;
        }

        public CarEntity GetCarById(int carId)
        {
            return unitOfWork.GetRepository().Query()
                .Include(c => c.Model)
                .ThenInclude(model => model.Brand)
                .FirstOrDefault(c => c.Id == carId);
        }

        public IEnumerable<CarInfoDto> GetAllByUserId(int userId)
        {
            return unitOfWork.GetRepository()
                .Query()
                .Where(car => car.OwnerId == userId)
                .Select(car => new CarInfoDto
                {
                    Id = car.Id,
                    BrandName = car.Model.Brand.Name,
                    ModelName = car.Model.Name,
                    Color = car.Color.ToString(),
                    PlateNumber = car.PlateNumber,
                });
        }
    }
}
