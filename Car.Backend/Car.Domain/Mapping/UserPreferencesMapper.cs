using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class UserPreferencesMapper : Profile
    {
        public UserPreferencesMapper()
        {
            CreateMap<UserPreferences, UserPreferencesDto>().ReverseMap();
        }
    }
}
