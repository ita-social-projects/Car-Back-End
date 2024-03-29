﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Extensions;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IRepository<CarEntity> carRepository;
        private readonly IRepository<User> userRepository;
        private readonly IImageService imageService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CarService(IRepository<CarEntity> carRepository, IRepository<User> userRepository, IImageService imageService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.carRepository = carRepository;
            this.userRepository = userRepository;
            this.imageService = imageService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateCarDto> AddCarAsync(CreateCarDto createCarModel)
        {
            var carEntity = mapper.Map<CreateCarDto, CarEntity>(createCarModel);
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            carEntity.OwnerId = userId;
            await imageService.UploadImageAsync(carEntity, createCarModel.Image);

            var newCar = await carRepository.AddAsync(carEntity);
            await carRepository.SaveChangesAsync();

            return mapper.Map<CarEntity, CreateCarDto>(newCar);
        }

        public async Task<CarDto> GetCarByIdAsync(int carId)
        {
            var car = await carRepository
                .Query()
                .FirstOrDefaultAsync(c => c.Id == carId);

            return mapper.Map<CarEntity, CarDto>(car);
        }

        public async Task<IEnumerable<CarDto>> GetAllByUserIdAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var cars = await carRepository
                .Query()
                .Where(car => car.OwnerId == userId)
                .ToListAsync();

            return mapper.Map<IEnumerable<CarEntity>, IEnumerable<CarDto>>(cars);
        }

        public async Task<(bool IsUpdated, UpdateCarDto? UpdatedCarDto)> UpdateCarAsync(UpdateCarDto updateCarModel)
        {
            var car = await carRepository.GetByIdAsync(updateCarModel.Id);

            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

            if (userId != car.OwnerId)
            {
                return (false, null);
            }

            await imageService.UpdateImageAsync(car, updateCarModel.Image);
            car.Color = updateCarModel.Color;
            car.Brand = updateCarModel.Brand;
            car.Model = updateCarModel.Model;
            car.PlateNumber = updateCarModel.PlateNumber;
            await carRepository.SaveChangesAsync();

            return (true, mapper.Map<CarEntity, UpdateCarDto>(car!));
        }

        public async Task<bool> DeleteAsync(int carId)
        {
            var car = await carRepository.GetByIdAsync(carId);

            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            if (userId != car.OwnerId)
            {
                return false;
            }

            carRepository.Delete(car);
            await carRepository.SaveChangesAsync();

            return true;
        }
    }
}
