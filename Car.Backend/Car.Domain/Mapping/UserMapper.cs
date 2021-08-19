using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UpdateUserImageDto, User>().ReverseMap();
            CreateMap<UpdateUserImageDto, UserDto>().ReverseMap();
            CreateMap<UpdateUserFcmtokenDto, UserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
