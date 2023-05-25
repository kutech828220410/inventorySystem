using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HIS_WebApi
{
    public class MessageHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        public void SendMessage(string message)
        {
            Task.Run(async () =>
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }).GetAwaiter().GetResult();
        }
    }
}
