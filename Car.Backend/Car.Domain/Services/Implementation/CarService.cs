using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Infrastructure;
using Car.Domain.Extensions;
using Car.Domain.Models;
using Car.Domain.Models.Car;
using Car.Domain.Services.Interfaces;
using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Implementation
{
    public class CarService : ICarService
    {
        // todo: move const to fileService logic
        private const string ImageContentType = "image/png";
        private readonly IRepository<CarEntity> carRepository;
        private readonly IFileService<File> fileService;
        private readonly IMapper mapper;

        public CarService(IRepository<CarEntity> carRepository, IFileService<File> fileService, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.fileService = fileService;
            this.mapper = mapper;
        }

        public async Task<CarEntity> AddCarAsync(CreateCarModel car)
        {
            var carEntity = mapper.Map<CreateCarModel, CarEntity>(car);
            if (car.Image != null)
            {
                carEntity.ImageId = await fileService.UploadFileAsync(car.Image.OpenReadStream(), car.Image.FileName, ImageContentType);
            }

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

            if (car?.ImageId != null)
            {
                car.ImageId = fileService.GetFileLinkAsync(car.ImageId);
            }

            return car;
        }

        public async Task<IEnumerable<CarEntity>> GetAllByUserIdAsync(int userId)
        {
            var cars = await carRepository
                .Query()
                .IncludeModelWithBrand()
                .Where(car => car.OwnerId == userId)
                .ToListAsync();

            foreach (var car in cars.Where(car => car.ImageId != null))
            {
                car.ImageId = fileService.GetFileLinkAsync(car.ImageId);
            }

            return cars;
        }

        public async Task<CarEntity> UpdateCarAsync(UpdateCarModel updateCarModel)
        {
            var car = await carRepository.GetByIdAsync(updateCarModel.Id);

            // todo: write ChangeImage(entity)
            if (!string.IsNullOrEmpty(car.ImageId))
            {
                await fileService.DeleteFileAsync(car.ImageId);
                car.ImageId = null;
            }

            car = mapper.Map<UpdateCarModel, CarEntity>(updateCarModel);
            if (updateCarModel.Image != null)
            {
                car.ImageId = await fileService.UploadFileAsync(
                    updateCarModel.Image.OpenReadStream(),
                    updateCarModel.Image.FileName,
                    ImageContentType);
            }

            await carRepository.SaveChangesAsync();

            return car;
        }
    }
}
