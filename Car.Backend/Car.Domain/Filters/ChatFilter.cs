using System.Collections.Generic;
using Car.Domain.Dto.Chat;

namespace Car.Domain.Filters
{
    public class ChatFilter
    {
        public string SearchText { get; set; } = string.Empty;

        public IEnumerable<ChatDto>? Chats { get; set; }
    }
}