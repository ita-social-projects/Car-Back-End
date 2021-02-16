using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Data.Entities.Car, CarDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Stop, StopDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<User, UserDto>();
            CreateMap<Journey, JourneyModel>();

            CreateMap<CarDto, Data.Entities.Car>();
            CreateMap<AddressDto, Address>();
            CreateMap<Stop, StopDto>();
            CreateMap<NotificationDto, Notification>();
            CreateMap<UserDto, User>();
            CreateMap<JourneyModel, Journey>();
        }
    }
}
