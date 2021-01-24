using System.Collections.Generic;
using Car.Domain.Dto;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Interfaces
{
    public interface ICarService
    {
        CarEntity GetCarById(int carId);

        CarEntity AddCar(CarDTO car);

        IEnumerable<CarInfoDTO> GetAllByUserId(int userId);
    }
}
