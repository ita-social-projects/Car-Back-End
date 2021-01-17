using Car.BLL.Dto;
using Car.BLL.Services.Interfaces;
using Car.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using CarEntity = Car.DAL.Entities.Car;

namespace Car.BLL.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork<CarEntity> unitOfWork;

        public CarService(IUnitOfWork<CarEntity> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public CarEntity AddCar(CarDTO carDTO)
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

        public IEnumerable<CarInfoDTO> GetAllByUserId(int userId)
        {
            return unitOfWork.GetRepository()
                .Query()
                .Where(car => car.UserId == userId)
                .Select(car => new CarInfoDTO
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
