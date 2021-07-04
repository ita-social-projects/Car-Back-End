using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class BrandMapper : Profile
    {
        public BrandMapper()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
        }
    }
}
