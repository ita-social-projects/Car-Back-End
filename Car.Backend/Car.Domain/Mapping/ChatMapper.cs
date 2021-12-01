using System.Linq;
using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto.Chat;

namespace Car.Domain.Mapping
{
    public class ChatMapper : Profile
    {
        public ChatMapper()
        {
            CreateMap<Chat, ChatDto>()
                .ForMember(destination => destination.JourneyOrganizer, options => options
                    .MapFrom(source => source.Journeys.First().Organizer))
                .ReverseMap();
            CreateMap<User, ChatUserDto>().ReverseMap();
            CreateMap<Journey, ChatJourneyDto>().ReverseMap();
            CreateMap<Chat, CreateChatDto>().ReverseMap();
            CreateMap<ReceivedMessages, ChatReceivedMessagesDto>().ReverseMap();
        }
    }
}
