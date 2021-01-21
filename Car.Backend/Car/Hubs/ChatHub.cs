using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Car.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("RecieveMessage", message);
        }
    }
}
