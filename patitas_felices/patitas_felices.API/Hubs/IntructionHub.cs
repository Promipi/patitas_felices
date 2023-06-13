using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace patitas_felices.API.Hubs
{
    public class IntructionHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",message);
        }
    }
}
