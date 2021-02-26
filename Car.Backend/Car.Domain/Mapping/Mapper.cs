using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateCarModel, CarEntity>();
            CreateMap<UpdateCarModel, CarEntity>();
            CreateMap<UpdateUserModel, User>();
            CreateMap<CarEntity, CarDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Stop, StopDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<User, UserDto>();
            CreateMap<Journey, JourneyModel>();

            CreateMap<CarDto, CarEntity>();
            CreateMap<AddressDto, Address>();
            CreateMap<Stop, StopDto>();
            CreateMap<NotificationDto, Notification>();
            CreateMap<UserDto, User>();
            CreateMap<JourneyModel, Journey>();
        }
    }
}
