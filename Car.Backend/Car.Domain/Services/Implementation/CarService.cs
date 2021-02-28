using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Interfaces;
using Car.Domain.Extensions;
using Car.Domain.Models;
using Car.Domain.Services.Interfaces;
using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Implementation
{
    public class CarService : ICarService
    {
        private const string ImageContentType = "image/png";
        private readonly IUnitOfWork<CarEntity> carUnitOfWork;
        private readonly IFileService<File> fileService;
        private readonly IMapper mapper;

        public CarService(IUnitOfWork<CarEntity> carUnitOfWork, IFileService<File> fileService, IMapper mapper)
        {
            this.carUnitOfWork = carUnitOfWork;
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

            var newCar = await carUnitOfWork.GetRepository().AddAsync(carEntity);
            await carUnitOfWork.SaveChangesAsync();

            return newCar;
        }

        public async Task<CarEntity> GetCarByIdAsync(int carId)
        {
            var car = await carUnitOfWork.GetRepository().Query()
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
            var cars = await carUnitOfWork.GetRepository()
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
            var car = carUnitOfWork.GetRepository().GetById(updateCarModel.Id);

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

            await carUnitOfWork.SaveChangesAsync();

            return car;
        }
    }
}
