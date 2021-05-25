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
            CreateMap<Stop, CreateStopModel>().ReverseMap();
            CreateMap<StopDto, CreateStopModel>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<AddressDto, CreateAddressModel>().ReverseMap();
            CreateMap<CreateAddressModel, Address>().ReverseMap();
            CreateMap<JourneyModel, Journey>().ReverseMap();
            CreateMap<CreateJourneyModel, Journey>().ReverseMap();
            CreateMap<CreateJourneyModel, JourneyModel>().ReverseMap();
            CreateMap<JourneyDto, Journey>().ReverseMap();
            CreateMap<JourneyDto, JourneyModel>().ReverseMap();
            CreateMap<JourneyPoint, JourneyPointDto>().ReverseMap();
            CreateMap<CreateJourneyPointModel, JourneyPoint>().ReverseMap();
            CreateMap<CreateJourneyPointModel, JourneyPointDto>().ReverseMap();
        }
    }
}
