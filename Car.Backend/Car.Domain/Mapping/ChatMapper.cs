using AutoMapper;
using Car.Data.Entities;
using Car.Domain.Models.Chat;

namespace Car.Domain.Mapping
{
    public class ChatMapper : Profile
    {
        public ChatMapper()
        {
            CreateMap<Chat, ChatModel>();
        }
    }
}
