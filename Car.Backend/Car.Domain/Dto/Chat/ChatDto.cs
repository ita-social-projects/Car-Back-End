using System.Collections.Generic;

namespace Car.Domain.Dto.Chat
{
    public class ChatDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string MessageText { get; set; } = string.Empty;

        public int MessageId { get; set; }

        public ICollection<ChatReceivedMessagesDto?>? ReceivedMessages { get; set; }

        public ChatUserDto? JourneyOrganizer { get; set; }

        public ChatJourneyDto? Journey { get; set; }
    }
}