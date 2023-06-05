using Microsoft.AspNetCore.SignalR;

namespace SignalR.Web.Hubs
{
    public class PuzzleHub : Hub
    {
        public async Task EnviarGanador()
        {
            await Clients.All.SendAsync("RecibirGanador");
        }
    }
}
