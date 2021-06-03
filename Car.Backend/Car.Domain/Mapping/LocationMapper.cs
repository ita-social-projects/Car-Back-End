﻿using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<LocationDto, Location>();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
