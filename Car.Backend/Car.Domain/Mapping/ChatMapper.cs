using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Dto.ChatDto;

namespace Car.Domain.Mapping
{
    public class ChatMapper : Profile
    {
        public ChatMapper()
        {
            CreateMap<Chat, ChatDto>().ReverseMap();
            CreateMap<User, ChatUserDto>().ReverseMap();
            CreateMap<Journey, ChatJourneyDto>().ReverseMap();
            CreateMap<Chat, CreateChatDto>().ReverseMap();
        }
    }
}
