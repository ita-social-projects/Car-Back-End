using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models.User;

namespace Car.Domain.Mapping
{
    public class JourneyUserMapper : Profile
    {
        public JourneyUserMapper()
        {
            CreateMap<JourneyUser, JourneyUserDto>().ReverseMap();
            CreateMap<JourneyUserModel, JourneyUser>().ReverseMap();
        }
    }
}
