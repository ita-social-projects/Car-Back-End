using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Infrastructure;
using Car.Domain.Extensions;
using Car.Domain.Models.Car;
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

        public async Task<CarEntity> AddCarAsync(CreateCarModel createCarModel)
        {
            var carEntity = mapper.Map<CreateCarModel, CarEntity>(createCarModel);
            await imageService.UploadImageAsync(carEntity, createCarModel.Image);

            var newCar = await carRepository.AddAsync(carEntity);
            await carRepository.SaveChangesAsync();

            return newCar;
        }

        public async Task<CarEntity> GetCarByIdAsync(int carId)
        {
            var car = await carRepository
                .Query()
                .IncludeModelWithBrand()
                .FirstOrDefaultAsync(c => c.Id == carId);
            imageService.SetImageLink(car);

            return car;
        }

        public async Task<IEnumerable<CarEntity>> GetAllByUserIdAsync(int userId)
        {
            var cars = await carRepository
                .Query()
                .IncludeModelWithBrand()
                .Where(car => car.OwnerId == userId)
                .ToListAsync();

            cars.ForEach((car) => imageService.SetImageLink(car));

            return cars;
        }

        public async Task<CarEntity> UpdateCarAsync(UpdateCarModel updateCarModel)
        {
            var car = await carRepository.GetByIdAsync(updateCarModel.Id);

            await imageService.UpdateImageAsync(car, updateCarModel.Image);
            car.Color = updateCarModel.Color;
            car.ModelId = updateCarModel.ModelId;
            car.PlateNumber = updateCarModel.PlateNumber;

            await carRepository.SaveChangesAsync();

            return car;
        }
    }
}
