using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<ModelDto, Model>().ReverseMap();
        }
    }
}
