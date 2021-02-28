using System;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Car.WebApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IChatService userManager;

        public SignalRHub(IChatService userManager)
        {
            this.userManager = userManager;
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
            var addedMessage = userManager.AddMessage(message);
            await Clients.Group(message.ChatId.ToString()).SendAsync("RecieveMessage", message);
        }
    }
}