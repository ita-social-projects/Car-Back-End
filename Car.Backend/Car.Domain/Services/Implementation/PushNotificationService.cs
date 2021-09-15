using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Helpers;
using Car.Domain.Services.Interfaces;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
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

        public async Task SendNotificationAsync(NotificationDto notification)
        {
            var reciever = userRepository.Query().Where(u => u.Id == notification.ReceiverId).Include(u => u.FCMTokens)
                                                                                             .FirstOrDefault();
            var sender = userRepository.Query().Where(u => u.Id == notification.SenderId).FirstOrDefault();
            if (reciever == null || sender == null)
            {
                return;
            }

            var (title, message) = NotificationsHelper.FormatToMessage(sender, notification);
            var data = new Dictionary<string, string>
            {
                { "navigateTab", "NotificationsTabs" },
            };
            foreach (var fcmToken in reciever.FCMTokens)
            {
                await firebaseService.SendAsync(CreateNotification(title, message, fcmToken.Token, data));
            }
        }

        public async Task SendNotificationAsync(model.Message message)
        {
            var chat = await chatRepository.Query()
                .Where(chat => chat.Id == message.ChatId)
                .Include(chat => chat.Journey)
                    .ThenInclude(jour => (jour != null) ? jour.Participants : null)
                    .ThenInclude(u => u.FCMTokens)
                .FirstOrDefaultAsync();

            if (chat?.Journey == null)
            {
                return;
            }

            var users = chat.Journey.Participants;
            var chatOwner = userRepository.Query().Where(u => u.Id == chat.Journey.OrganizerId).FirstOrDefault();

            if (chatOwner != null)
            {
                users.Add(chatOwner);
            }

            var data = new Dictionary<string, string>
            {
                { "navigateTab", "Chat" },
                { "chatId", chat.Id.ToString() },
            };
            foreach (var user in users)
            {
                if (user.Id != message.Sender?.Id)
                {
                    var senderName = (message.Sender != null) ? message.Sender.Name : "User";
                    foreach (var fcmToken in user.FCMTokens)
                    {
                        await firebaseService.SendAsync(CreateNotification(senderName, message.Text, fcmToken.Token, data));
                    }
                }
            }
        }

        private static Message CreateNotification(string title, string notificationBody, string token, Dictionary<string, string> data)
        {
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title,
                },
                Data = data,
            };
        }
    }
}