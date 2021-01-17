using Car.BLL.Dto;
using System.Collections.Generic;
using CarEntity = Car.DAL.Entities.Car;

namespace Car.BLL.Services.Interfaces
{
    public interface ICarService
    {
        CarEntity GetCarById(int carId);

        CarEntity AddCar(CarDTO car);

        IEnumerable<CarInfoDTO> GetAllByUserId(int userId);
    }
}
