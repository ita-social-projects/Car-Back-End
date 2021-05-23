namespace Car.Domain.Dto.ChatDto
{
    public class ChatDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MessageText { get; set; }

        public ChatUserDto JourneyOrganizer { get; set; }

        public ChatJourneyDto Journey { get; set; }
    }
}