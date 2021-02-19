using System.Collections.Generic;
using Car.Domain.Dto;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Interfaces
{
    public interface ICarService
    {
        CarDto GetCarById(int carId);

        CarEntity AddCar(CarEntity car);

        IEnumerable<CarDto> GetAllByUserId(int userId);

        CarEntity UpdateCar(CarEntity car);
    }
}
