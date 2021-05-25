using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;

namespace Car.Domain.Mapping
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<Request, JourneyFilterModel>()
                .ForMember(f => f.FromLatitude, req => req.MapFrom(r => r.From.Latitude))
                .ForMember(f => f.FromLongitude, req => req.MapFrom(r => r.From.Longitude))
                .ForMember(f => f.ToLatitude, req => req.MapFrom(r => r.To.Latitude))
                .ForMember(f => f.ToLongitude, req => req.MapFrom(r => r.To.Longitude))
                .ReverseMap();
        }
    }
}
