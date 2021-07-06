using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model = Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Hubs;
using Car.Domain.Services.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseApp app;
        private readonly FirebaseMessaging messaging;
        private readonly IHubContext<SignalRHub> hub;
        private readonly IRepository<model.Chat> chatRepository;
        private readonly IRepository<model.User> userRepository;
        private readonly IRepository<model.Journey> journeyRepository;

        public FirebaseService(
            IHubContext<SignalRHub> hub,
            IRepository<model.Chat> chatRepository,
            IRepository<model.Journey> journeyRepository,
            IRepository<model.User> userRepository)
        {
            this.hub = hub;
            this.chatRepository = chatRepository;
            this.journeyRepository = journeyRepository;
            this.userRepository = userRepository;

            if (FirebaseApp.GetInstance("[DEFAULT]") == null)
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential
                        .FromFile(@"..\..\Car.Backend\Car.WebApi\wwwroot\Credentials\credential-firebase-adminsdk.json")
                        .CreateScoped("https://www.googleapis.com/auth/firebase.messaging"),
                });
                messaging = FirebaseMessaging.GetMessaging(app);
            }
            else
            {
                app = FirebaseApp.GetInstance("[DEFAULT]");
                messaging = FirebaseMessaging.GetMessaging(app);
            }
        }

        public async Task SendNotification(model.Notification notification)
        {
            var reciever = userRepository.Query().Where(u => u.Id == notification.ReceiverId).FirstOrDefault();
            var sender = userRepository.Query().Where(u => u.Id == notification.SenderId).FirstOrDefault();
            if (reciever.FCMToken == null)
            {
                return;
            }

            var (title, message) = FormatToMessage(sender, notification);
            await messaging.SendAsync(CreateNotification(title, message, reciever.FCMToken, "NotificationsTabs"));
        }

        public async Task SendNotification(model.Message message)
        {
            var chat = await chatRepository.Query()
                .Where(chat => chat.Id == message.ChatId)
                .Include(chat => chat.Journey)
                    .ThenInclude(jour => jour.Participants)
                .FirstOrDefaultAsync();

            var users = chat.Journey.Participants;
            var chatOwner = userRepository.Query().Where(u => u.Id == chat.Journey.OrganizerId).FirstOrDefault();

            users.Add(chatOwner);
            foreach (var user in users)
            {
                if (user.FCMToken != null)
                {
                    try
                    {
                        //pls create method to send motification to group of users. this - user.FCMToken = 1 ; FCMToken[] = many
                        await messaging.SendAsync(CreateNotification(message.Sender.Name, message.Text, user.FCMToken, "MessagesTabs"));
                    }
                    catch (FirebaseMessagingException ex)
                    {
                        System.Console.WriteLine($"{user.Name} has a problem with token : " + ex);
                        user.FCMToken = null;
                        await userRepository.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task UpdateUserToken(string token, int id)
        {
            var user = userRepository.Query().Where(user => user.Id == id && user.FCMToken != token).FirstOrDefault();

            if (user == null)
            {
                return;
            }

            user.FCMToken = token;
            await userRepository.SaveChangesAsync();
            System.Console.WriteLine(user.Name + " : " + user.Token);
        }

        private Message CreateNotification(string title, string notificationBody, string token, string url)
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
                    {
                        //here`s a string which`ll in future redirect user to correct page on notification click
                        "url", url
                    }
                }
            };
        }

        private (string title, string message) FormatToMessage(model.User sender, model.Notification notification)
        {
            var (title, message) = notification.Type switch
            {
                model.NotificationType.PassengerApply
                    => ("Your ride", $"{sender.Name} whants to join a ride"),

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

                _ => throw new System.NotImplementedException()
            };
            return (title, message);
        }
    }
}