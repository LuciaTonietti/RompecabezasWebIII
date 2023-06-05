using Microsoft.AspNetCore.SignalR;

namespace Rompecabezas.Web.Hubs
{
    public class SalaHub : Hub
    {
        public async Task UnirseASala(string nroSala, string usuario)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, nroSala);
            await Clients.Group(nroSala).SendAsync("RecibirMensaje", "Sistema", $"{usuario} se unió a la sala");
        }

        public async Task SalirDeSala(string nroSala, string usuario)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, nroSala);
            await Clients.Group(nroSala).SendAsync("RecibirMensaje", "Sistema", $"{usuario} salió de la sala");
        }
    }
}
