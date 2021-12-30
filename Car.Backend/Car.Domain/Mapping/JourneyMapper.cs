using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Dto.Address;
using Car.Domain.Dto.Journey;
using Car.Domain.Models.Journey;

namespace Car.Domain.Mapping
{
    public class JourneyMapper : Profile
    {
        public JourneyMapper()
        {
            CreateMap<Stop, StopDto>().ReverseMap();
            CreateMap<Invitation, InvitationDto>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<JourneyModel, Journey>().ReverseMap();
            CreateMap<ScheduleModel, Schedule>().ReverseMap();
            CreateMap<JourneyDto, Journey>().ReverseMap();
            CreateMap<JourneyDto, JourneyModel>().ReverseMap();
            CreateMap<JourneyPoint, JourneyPointDto>().ReverseMap();
            CreateMap<ScheduleDto, Schedule>().ReverseMap();
        }
    }
}
