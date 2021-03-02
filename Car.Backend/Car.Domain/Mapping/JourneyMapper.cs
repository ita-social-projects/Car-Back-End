using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;

namespace Car.Domain.Mapping
{
    public class JourneyMapper : Profile
    {
        public JourneyMapper()
        {
            CreateMap<Stop, StopDto>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<JourneyModel, Journey>().ReverseMap();
        }
    }
}
