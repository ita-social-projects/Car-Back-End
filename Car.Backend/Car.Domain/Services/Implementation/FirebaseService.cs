using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Car.Domain.Configurations;
using Car.Domain.Services.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Car.Domain.Services.Implementation
{
    public class FirebaseService : IFirebaseService
    {

        private FirebaseMessaging Messaging { get; set; }

        private FirebaseApp App { get; set; }

        public FirebaseService(
            IWebHostEnvironment webHostEnvironment,
            IOptions<FirebaseOptions> firebaseOptions)
        {
            var keyFilePath = Path.Combine(
                webHostEnvironment.WebRootPath,
                firebaseOptions.Value.CredentialsPath!);

            using var stream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential.FromStream(stream);
            credential = credential.CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

            App = FirebaseApp.Create(new AppOptions()
            {
                Credential = credential,
            });

            Messaging = FirebaseMessaging.GetMessaging(App);
        }

        public async Task<List<bool>> SendAsync(MulticastMessage message)
        {
            return (await Messaging.SendMulticastAsync(message)).Responses.Select(resp => resp.IsSuccess).ToList();
        }
    }
}
