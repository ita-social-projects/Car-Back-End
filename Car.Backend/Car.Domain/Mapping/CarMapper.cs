using AutoMapper;
using Car.Domain.Dto;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Mapping
{
    public class CarMapper : Profile
    {
        public CarMapper()
        {
            CreateMap<CreateCarDto, CarEntity>().ReverseMap();
            CreateMap<UpdateCarDto, CarEntity>().ReverseMap();
            CreateMap<CarDto, CarEntity>().ReverseMap();
        }
    }
}