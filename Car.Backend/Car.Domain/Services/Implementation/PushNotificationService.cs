using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Infrastructure;
using Car.Domain.Configurations;
using Car.Domain.Dto;
using Car.Domain.Hubs;
using Car.Domain.Services.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using model = Car.Data.Entities;

namespace Car.Domain.Services.Implementation
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IRepository<model.Chat> chatRepository;
        private readonly IRepository<model.User> userRepository;
        private readonly IFirebaseService firebaseService;

        public PushNotificationService(
            IRepository<model.Chat> chatRepository,
            IRepository<model.User> userRepository,
            IFirebaseService firebaseService)
        {
            this.chatRepository = chatRepository;
            this.userRepository = userRepository;
            this.firebaseService = firebaseService;
        }

        public async Task<string?> SendNotificationAsync(NotificationDto notification)
        {
            var reciever = userRepository.Query().Where(u => u.Id == notification.ReceiverId).FirstOrDefault();
            var sender = userRepository.Query().Where(u => u.Id == notification.SenderId).FirstOrDefault();
            if (reciever == null || reciever.FCMToken == null || sender == null)
            {
                return null;
            }

            var (title, message) = FormatToMessage(sender, notification);
            return await firebaseService.SendAsync(CreateNotification(title, message, reciever.FCMToken, "NotificationsTabs"));
        }

        public async Task<bool> SendNotificationAsync(model.Message message)
        {
            var chat = await chatRepository.Query()
                .Where(chat => chat.Id == message.ChatId)
                .Include(chat => chat.Journey)
                    .ThenInclude(jour => (jour != null) ? jour.Participants : null)
                .FirstOrDefaultAsync();

            if (chat == null || chat.Journey == null)
            {
                return false;
            }

            var users = chat.Journey.Participants;
            var chatOwner = userRepository.Query().Where(u => u.Id == chat.Journey.OrganizerId).FirstOrDefault();

            if (chatOwner != null)
            {
                users.Add(chatOwner);
            }

            foreach (var user in users)
            {
                if (user.FCMToken != null && user.Id != message.Sender?.Id)
                {
                    await firebaseService.SendAsync(
                        CreateNotification((message.Sender != null) ? message.Sender.Name : "User", message.Text, user.FCMToken, "MessagesTabs"));
                }
            }

            return true;
        }

        private static Message CreateNotification(string title, string notificationBody, string token, string navigateTab)
        {
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title,
                },
                Data = new Dictionary<string, string>
                {
                    { "navigateTab", navigateTab },
                },
            };
        }

        private static (string Title, string Message) FormatToMessage(model.User sender, NotificationDto notification)
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