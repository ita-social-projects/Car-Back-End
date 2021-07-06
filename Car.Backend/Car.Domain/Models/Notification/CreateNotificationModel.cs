using Car.Data.Entities;

namespace Car.Domain.Models.Notification
{
    public class CreateNotificationModel
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public NotificationType Type { get; set; }

        public string JsonData { get; set; } = "{}";

        public int? JourneyId { get; set; }
    }
}
