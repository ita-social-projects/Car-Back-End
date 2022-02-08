using System;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.SignalR;

namespace Car.Domain.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IChatService userManager;
        private readonly IPushNotificationService pushNotificationService;

        public SignalRHub(
            IChatService userManager,
            IPushNotificationService pushNotificationService)
        {
            this.userManager = userManager;
            this.pushNotificationService = pushNotificationService;
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
            await pushNotificationService.SendNotificationAsync(message);
        }

        public async Task EnterBadges(string id)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                id);
        }

        public async Task LeaveBadges(string id)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                id);
        }
    }
}