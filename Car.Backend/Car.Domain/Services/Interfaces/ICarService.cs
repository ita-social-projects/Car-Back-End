using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Models;
using Car.Domain.Models.Car;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarEntity> GetCarByIdAsync(int carId);

        Task<CarEntity> AddCarAsync(CreateCarModel car);

        Task<IEnumerable<CarEntity>> GetAllByUserIdAsync(int userId);

        Task<CarEntity> UpdateCarAsync(UpdateCarModel car);
    }
}
