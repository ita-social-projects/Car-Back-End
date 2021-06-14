using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
