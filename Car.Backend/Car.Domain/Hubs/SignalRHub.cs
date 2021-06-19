using System;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Car.Domain.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IChatService userManager;
        private readonly IFirebaseService firebaseService;
        private readonly IRepository<User> userRepository;

        public SignalRHub(
            IChatService userManager,
            IFirebaseService firebaseService,
            IRepository<User> userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.firebaseService = firebaseService;
        }

        public async Task EnterToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveTheGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(Message message)
        {
            message.CreatedAt = DateTime.UtcNow;
            await userManager.AddMessageAsync(message);
            await Clients.Group(message.ChatId.ToString()).SendAsync("RecieveMessage", message);
        }

        public async Task UpdateFCMTocken(string token, int userId)
        {
            System.Console.WriteLine(userId + " : " + token);
            await firebaseService.UpdateUserToken(userRepository, token, userId);
        }
    }
}