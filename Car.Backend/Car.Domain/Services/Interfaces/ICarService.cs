using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Dto;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarDto> GetCarByIdAsync(int carId);

        Task<CreateCarDto> AddCarAsync(CreateCarDto createCarModel);

        Task<IEnumerable<CarDto>> GetAllByUserIdAsync();

        Task<(bool IsUpdated, UpdateCarDto? UpdatedCarDto)> UpdateCarAsync(UpdateCarDto updateCarModel);

        Task<bool> DeleteAsync(int carId);
    }
}
