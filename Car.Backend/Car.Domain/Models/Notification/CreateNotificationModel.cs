using Car.Data.Entities;

namespace Car.Domain.Models.Notification
{
    public class CreateNotificationModel
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public NotificationType Type { get; set; }

<<<<<<< HEAD
        public string JsonData { get; set; }

        public int? JourneyId { get; set; }
=======
        public string JsonData { get; set; } = "{}";
>>>>>>> ee067aa6ef45f4d099c648db6f1b2d4c0d7c9133
    }
}
