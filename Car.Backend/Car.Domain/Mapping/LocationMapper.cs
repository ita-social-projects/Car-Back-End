using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Models.Journey;
using Car.Domain.Models.Location;

namespace Car.Domain.Mapping
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<CreateLocationModel, Location>();
            CreateMap<CreateAddressModel, Address>().ReverseMap();
        }
    }
}
