using Microsoft.AspNetCore.SignalR;
using Rompecabezas.Logica.Entidades;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SignalR.Web.Hubs
{
    public class PuzzleHub : Hub
    {
        private static List<Jugador> _jugadoresConectados = new List<Jugador>();
        public async Task EnviarGanador(string ganador)
        {
            await Clients.Others.SendAsync("RecibirGanador", ganador);
        }

        public async Task AgregarJugador(string nombreJugador)
        {
            Jugador jugador = new Jugador();
            jugador.NickName = nombreJugador;
            jugador.Score = 0;
            jugador.IdConexion = Context.ConnectionId;
            _jugadoresConectados.Add(jugador);
            await Clients.All.SendAsync("ActualizarListaJugadores", _jugadoresConectados);
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            string connectionId = Context.ConnectionId;
            Jugador jugadorAEliminar = _jugadoresConectados.FirstOrDefault(jugador => jugador.IdConexion == connectionId);
            _jugadoresConectados.Remove(jugadorAEliminar);
            await Clients.All.SendAsync("ActualizarListaJugadores", _jugadoresConectados);
        }

        public async Task SumarPuntaje()
        {
            string connectionId = Context.ConnectionId;
            Jugador jugador = _jugadoresConectados.FirstOrDefault(jugador => jugador.IdConexion == connectionId);
            jugador.Score += 3;
            _jugadoresConectados.Sort((jugador1, jugador2) => jugador2.Score.CompareTo(jugador1.Score));
            await Clients.All.SendAsync("ActualizarListaJugadores", _jugadoresConectados);
        }

        public async Task RestarPuntaje()
        {
            string connectionId = Context.ConnectionId;
            Jugador jugador = _jugadoresConectados.FirstOrDefault(jugador => jugador.IdConexion == connectionId);
            if(jugador.Score > 0)
                jugador.Score --;
            _jugadoresConectados.Sort((jugador1, jugador2) => jugador2.Score.CompareTo(jugador1.Score));
            await Clients.All.SendAsync("ActualizarListaJugadores", _jugadoresConectados);
        }
    }
}
