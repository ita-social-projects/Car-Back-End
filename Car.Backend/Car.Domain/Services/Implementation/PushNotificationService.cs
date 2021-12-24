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
        private readonly IUserService userService;

        public PushNotificationService(
            IRepository<model.Chat> chatRepository,
            IRepository<model.User> userRepository,
            IFirebaseService firebaseService,
            IUserService userService)
        {
            this.chatRepository = chatRepository;
            this.userRepository = userRepository;
            this.firebaseService = firebaseService;
            this.userService = userService;
        }

        public async Task SendNotificationAsync(NotificationDto notification)
        {
            var reciever = userRepository
                .Query()
                .Where(u => u.Id == notification.ReceiverId)
                .Include(u => u.FCMTokens)
                .FirstOrDefault();

            var sender = userRepository
                .Query()
                .Where(u => u.Id == notification.SenderId)
                .FirstOrDefault();

            if (reciever == null || sender == null)
            {
                return;
            }

            var (title, message) = NotificationsHelper.FormatToMessage(sender, notification);
            var data = new Dictionary<string, string>
            {
                { "navigateTab", "NotificationsTabs" },
            };
            var recieverTokens = reciever.FCMTokens.Select(t => t.Token);

            if (recieverTokens.Any())
            {
                var response = await firebaseService.SendAsync(CreateNotification(title, message, recieverTokens, data));
                await DeleteIncorrectFcmTokensFromResponse(response, recieverTokens.ToList().AsReadOnly());
            }
        }

        public async Task SendNotificationAsync(model.Message message)
        {
            var chat = await chatRepository.Query()
                .Where(chat => chat.Id == message.ChatId)
                .Include(chat => chat.Journeys)
                    .ThenInclude(jour => (jour != null) ? jour.Participants : null)
                    .ThenInclude(u => u.FCMTokens)
                .Include(chat => chat.Journeys)
                    .ThenInclude(jour => (jour != null) ? jour.Organizer : null)
                    .ThenInclude(u => u.FCMTokens)
                .FirstOrDefaultAsync();

            if (chat?.Journeys == null || chat?.Journeys.Any() == false)
            {
                return;
            }

            var users = chat!.Journeys.SelectMany(j => j.Participants)
                .Union(chat.Journeys.Select(j => j.Organizer))
                .Where(u => u != null)
                .Distinct();

            var data = new Dictionary<string, string>
            {
                { "navigateTab", "Chat" },
                { "chatId", chat.Id.ToString() },
            };
            foreach (var user in users)
            {
                if (user!.Id != message.Sender?.Id)
                {
                    var senderName = (message.Sender != null) ? message.Sender.Name : "User";
                    var recieverTokens = user.FCMTokens.Select(t => t.Token);
                    if (recieverTokens.Any())
                    {
                        var response = await firebaseService.SendAsync(CreateNotification(senderName, message.Text, recieverTokens, data));
                        await DeleteIncorrectFcmTokensFromResponse(response, recieverTokens.ToList().AsReadOnly());
                    }
                }
            }
        }

        private static MulticastMessage CreateNotification(string title, string notificationBody, IEnumerable<string> tokens, Dictionary<string, string> data)
        {
            return new MulticastMessage()
            {
                Tokens = tokens.ToList().AsReadOnly(),
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title,
                },
                Data = data,
            };
        }

        private async Task DeleteIncorrectFcmTokensFromResponse(List<bool> response, IReadOnlyList<string> tokens)
        {
            for (var i = 0; i < response.Count; i++)
            {
                if (!response[i])
                {
                    await userService.DeleteUserFcmtokenAsync(tokens[i]);
                }
            }
        }
    }
}