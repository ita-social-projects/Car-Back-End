using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Dto.Address;
using Car.Domain.Dto.Location;

namespace Car.Domain.Mapping
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<LocationDto, Location>();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<UpdateLocationDto, Location>();
            CreateMap<UpdateAddressToLocationDto, Address>().ReverseMap();
        }
    }
}
