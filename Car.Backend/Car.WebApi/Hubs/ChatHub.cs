using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Car.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message) =>
            await Clients.All.SendAsync("RecieveMessage", message);
    }
}
