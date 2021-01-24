using System.Collections.Generic;
using System.Linq;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
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

        public CarEntity AddCar(CarDto carDTO)
        {
            var newCar = new CarEntity
            {
                BrandId = carDTO.BrandId,
                ModelId = carDTO.ModelId,
                Color = carDTO.Color.ToString(),
                PlateNumber = carDTO.PlateNumber,
                UserId = carDTO.UserId,
            };

            var car = unitOfWork.GetRepository().Add(newCar);
            unitOfWork.SaveChanges();

            return car;
        }

        public CarEntity GetCarById(int carId)
        {
            return unitOfWork.GetRepository().GetById(carId);
        }

        public IEnumerable<CarInfoDto> GetAllByUserId(int userId)
        {
            return unitOfWork.GetRepository()
                .Query()
                .Where(car => car.UserId == userId)
                .Select(car => new CarInfoDto
                {
                    Id = car.Id,
                    BrandName = car.Brand.Name,
                    ModelName = car.Model.Name,
                    Color = car.Color,
                    PlateNumber = car.PlateNumber,
                });
        }
    }
}
