using System.Collections.Generic;
using Car.Domain.Dto.ChatDto;

namespace Car.Domain.Filters
{
    public class ChatFilter
    {
        public string SearchText { get; set; }

        public IEnumerable<ChatDto> Chats { get; set; }
    }
}