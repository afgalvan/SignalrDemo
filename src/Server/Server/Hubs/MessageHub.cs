using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SharedLib;

namespace Server.Hubs
{
    public class MessageHub : Hub
    {
        public async Task Send(MessageDto data)
        {
            if (string.IsNullOrEmpty(data.Content)) return;
            await Clients.Others.SendAsync("Send", data);
        }
    }
}
