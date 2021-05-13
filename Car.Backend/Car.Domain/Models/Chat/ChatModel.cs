using System.Collections.Generic;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;

namespace Car.Domain.Models.Chat
{
    public class ChatModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public JourneyModel Journey { get; set; }

        public ICollection<MessageDto> Messages { get; set; } = new List<MessageDto>();
    }
}