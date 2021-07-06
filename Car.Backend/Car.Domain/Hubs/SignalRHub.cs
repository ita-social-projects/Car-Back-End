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

        public SignalRHub(
            IChatService userManager,
            IFirebaseService firebaseService)
        {
            this.userManager = userManager;
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
            await firebaseService.SendNotification(message);
        }

        public async Task UpdateFCMTocken(string token, int userId)
        {
            await firebaseService.UpdateUserToken(token, userId);
        }
    }
}