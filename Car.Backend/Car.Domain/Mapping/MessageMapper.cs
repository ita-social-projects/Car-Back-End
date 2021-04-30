using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Mapping
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Sender.Id))
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Name))
            .ForMember(dest => dest.SenderSurname, opt => opt.MapFrom(src => src.Sender.Surname))
            .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.Sender.ImageId));
        }
    }
}
