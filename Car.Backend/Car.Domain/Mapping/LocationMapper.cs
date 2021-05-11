using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models.Location;

namespace Car.Domain.Mapping
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<CreateLocationModel, Location>();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }
}
