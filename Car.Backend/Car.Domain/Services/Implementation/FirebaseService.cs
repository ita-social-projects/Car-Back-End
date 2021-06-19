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

namespace Car.Domain.Services.Implementation
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseApp app;
        private readonly FirebaseMessaging messaging;
        private readonly IHubContext<SignalRHub> hub;

        public FirebaseService(IHubContext<SignalRHub> hub)
        {
            this.hub = hub;

            if (FirebaseApp.GetInstance("[DEFAULT]") == null)
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(@"C:\Users\illyich_\Desktop\SoftServe\Car-Back-End\Car-Back-End\Car.Backend\Car.WebApi\wwwroot\Credentials\credential-firebase-adminsdk.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging"),
                });
                messaging = FirebaseMessaging.GetMessaging(app);
            }
        }

        public async Task SendNotification(IRepository<model.User> userRepository, int id, string title, string body)
        {
            var user = userRepository.Query().Where(user => user.Id == id).FirstOrDefault();
            if (user.FCMToken != null)
            {
                string result = string.Empty;

                result = await messaging.SendAsync(CreateNotification(title, body, user.FCMToken));
                System.Console.WriteLine("notification sent ? : " + result);
            }
        }

        public async Task UpdateUserToken(IRepository<model.User> userRepository, string token, int id)
        {
            var user = userRepository.Query().Where(user => user.Id == id && user.FCMToken != token).FirstOrDefault();
            if (user != null)
            {
                user.FCMToken = token;
                await userRepository.SaveChangesAsync();
                System.Console.WriteLine(user.Name + " : " + user.Token);
            }
        }

        private Message CreateNotification(string title, string notificationBody, string token)
        {
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
            };
        }
    }
}