﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public FirebaseApp App { get; set; }

        public FirebaseMessaging Messaging { get; set; }

        public FirebaseService(
            IWebHostEnvironment webHostEnvironment,
            IOptions<FirebaseOptions> firebaseOptions)
        {
            var keyFilePath = Path.Combine(
                webHostEnvironment.WebRootPath,
                firebaseOptions.Value.CredentialsPath!);

            var stream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential.FromStream(stream);
            credential = credential.CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

            App = FirebaseApp.Create(new AppOptions()
            {
                Credential = credential,
            });
            Messaging = FirebaseMessaging.GetMessaging(App);
        }

        public async Task<string> SendAsync(Message message)
        {
            return await Messaging.SendAsync(message);
        }
    }
}
