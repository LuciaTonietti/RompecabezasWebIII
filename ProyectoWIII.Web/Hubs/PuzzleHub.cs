using Microsoft.AspNetCore.SignalR;

namespace SignalR.Web.Hubs
{
    public class PuzzleHub : Hub
    {
        public async Task EnviarGanador(string ganador)
        {
            await Clients.Others.SendAsync("RecibirGanador", ganador);
        }
    }
}
