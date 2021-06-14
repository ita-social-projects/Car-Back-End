namespace Car.Domain.Dto.ChatDto
{
    public class ChatDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string MessageText { get; set; } = string.Empty;

        public ChatUserDto? JourneyOrganizer { get; set; }

        public ChatJourneyDto? Journey { get; set; }
    }
}