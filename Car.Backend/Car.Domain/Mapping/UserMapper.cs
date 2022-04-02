using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Dto.User;

namespace Car.Domain.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UpdateUserImageDto, User>().ReverseMap();
            CreateMap<UpdateUserImageDto, UserDto>().ReverseMap();
            CreateMap<UserFcmTokenDto, FcmToken>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserEmailDto>().ReverseMap();
            CreateMap<UserStop, UserStopDto>().ReverseMap();
        }
    }
}
