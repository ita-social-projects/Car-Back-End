using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Extensions;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IRepository<CarEntity> carRepository;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public CarService(IRepository<CarEntity> carRepository, IImageService imageService, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        public async Task<CreateCarDto> AddCarAsync(CreateCarDto createCarModel)
        {
            var carEntity = mapper.Map<CreateCarDto, CarEntity>(createCarModel);
            await imageService.UploadImageAsync(carEntity, createCarModel.Image);

            var newCar = await carRepository.AddAsync(carEntity);
            await carRepository.SaveChangesAsync();

            return mapper.Map<CarEntity, CreateCarDto>(newCar);
        }

        public async Task<CarEntity> GetCarByIdAsync(int carId)
        {
            var car = await carRepository
                .Query()
                .IncludeModelWithBrand()
                .FirstOrDefaultAsync(c => c.Id == carId);

            return car;
        }

        public async Task<IEnumerable<CarEntity>> GetAllByUserIdAsync(int userId)
        {
            var cars = await carRepository
                .Query()
                .IncludeModelWithBrand()
                .Where(car => car.OwnerId == userId)
                .ToListAsync();

            return cars;
        }

        public async Task<UpdateCarDto> UpdateCarAsync(UpdateCarDto updateCarModel)
        {
            var car = await carRepository.GetByIdAsync(updateCarModel.Id);

            if (car != null)
            {
                await imageService.UpdateImageAsync(car, updateCarModel.Image);
                car.Color = updateCarModel.Color;
                car.ModelId = updateCarModel.ModelId;
                car.PlateNumber = updateCarModel.PlateNumber;
            }

            await carRepository.SaveChangesAsync();

            return mapper.Map<CarEntity, UpdateCarDto>(car!);
        }

        public async Task DeleteAsync(int carId)
        {
            carRepository.Delete(new CarEntity() { Id = carId });
            await carRepository.SaveChangesAsync();
        }
    }
}
