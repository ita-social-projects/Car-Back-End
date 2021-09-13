using Car.Domain.Dto;
using model = Car.Data.Entities;

namespace Car.Domain.Helpers
{
    public static class NotificationsHelper
    {
        public static (string Title, string Message) FormatToMessage(model.User sender, NotificationDto notification)
        {
            var (title, message) = notification.Type switch
            {
                model.NotificationType.PassengerApply
                    => ("Your ride", $"{sender.Name} wants to join a ride"),

                model.NotificationType.ApplicationApproval
                    => ($"{sender.Name}`s ride", "Your request has been approved"),

                model.NotificationType.JourneyCancellation
                    => ($"{sender.Name}`s ride", $"{sender.Name}`s ride has been canceled"),

                model.NotificationType.JourneyDetailsUpdate
                    => ($"{sender.Name}`s ride", $"{sender.Name}`s ride has been updated"),

                model.NotificationType.JourneyInvitation
                    => ($"You recieved a ride invite", $"{sender.Name}, invited you to join a ride"),

                model.NotificationType.AcceptedInvitation
                    => ("Your journey", $"{sender.Name} accepted your invitation"),

                model.NotificationType.RejectedInvitation
                    => ("Your journey", $"{sender.Name} rejected your invitation"),

                model.NotificationType.PassengerWithdrawal
                    => ("Your journey", $"{sender.Name} withdrawed your request"),

                model.NotificationType.RequestedJourneyCreated
                    => ("Your journey", $"{sender.Name} created requested journey"),

                model.NotificationType.ApplicationRejection
                    => ("Your journey", $"{sender.Name} rejected your application"),

                _ => ("Car", $"You have new notification from {sender.Name}"),
            };
            return (title, message);
        }
    }
}
