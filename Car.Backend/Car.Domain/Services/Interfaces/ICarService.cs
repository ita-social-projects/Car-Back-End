using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Models.Car;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarEntity> GetCarByIdAsync(int carId);

        Task<CreateCarDto> AddCarAsync(CreateCarDto createCarModel);

        Task<IEnumerable<CarEntity>> GetAllByUserIdAsync(int userId);

        Task<UpdateCarDto> UpdateCarAsync(UpdateCarDto updateCarModel);

        Task DeleteAsync(int carId);
    }
}
