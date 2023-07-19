using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace patitas_felices.API.Hubs
{
    public class InstructionHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage",message);
        }

    }
}
