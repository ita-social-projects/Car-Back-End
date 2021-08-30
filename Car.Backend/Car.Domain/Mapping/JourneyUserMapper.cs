using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class JourneyUserMapper : Profile
    {
        public JourneyUserMapper()
        {
            CreateMap<JourneyUser, JourneyUserDto>().ReverseMap();
        }
    }
}
