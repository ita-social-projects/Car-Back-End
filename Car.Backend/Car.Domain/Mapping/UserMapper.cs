using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models;
using Car.Domain.Models.User;

namespace Car.Domain.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UpdateUserModel, User>();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
