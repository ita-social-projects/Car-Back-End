using Car.Data.Entities;

namespace Car.Domain.Dto
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public NotificationType Type { get; set; }

        public string JsonData { get; set; }
    }
}