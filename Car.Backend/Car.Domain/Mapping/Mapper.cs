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
            CreateMap<Journey, JourneyDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<User, UserDto>();
        }
    }
}
