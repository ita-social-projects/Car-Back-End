using AutoMapper;
using Car.Domain.Dto;
using Car.Domain.Models;
using Car.Domain.Models.Car;
using CarEntity = Car.Data.Entities.Car;

namespace Car.Domain.Mapping
{
    public class CarMapper : Profile
    {
        public CarMapper()
        {
            CreateMap<CreateCarModel, CarEntity>();
            CreateMap<UpdateCarModel, CarEntity>();
            CreateMap<CarEntity, CarDto>().ReverseMap();
        }
    }
}
