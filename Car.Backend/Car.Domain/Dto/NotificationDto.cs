using Car.Data.Entities;

namespace Car.Domain.Dto
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Position { get; set; }

        public int ReceiverId { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public string CreateAt { get; set; }

        public int JourneyId { get; set; }

        public string UserColor { get; set; }

        public NotificationType NotificationType { get; set; }
    }
}
