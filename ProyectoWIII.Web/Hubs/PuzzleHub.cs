using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Protocols;
using Rompecabezas.Logica.Entidades;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SignalR.Web.Hubs
{
    public class PuzzleHub : Hub
    {
        private static List<Jugador> _jugadoresConectados = new List<Jugador>();
        private static Dictionary<int, List<Jugador>> _salaJugadores = new Dictionary<int, List<Jugador>>();
        private static Dictionary<string, int> _salaConecctionId = new Dictionary<string, int>();
        
        
        
        
        
        public async Task EnviarGanador(string ganador, int nroSala)
        {
            await Clients.OthersInGroup(nroSala.ToString()).SendAsync("RecibirGanador", ganador);
        }

        
        
        public async Task AgregarJugador(string nombreJugador, int nroSala)
        {
            try
            {
                string connectionId = Context.ConnectionId;
                List<Jugador> jugadoresContectados = obtenerJugadoresSala(nroSala);
                Jugador jugador = new Jugador();
                jugador.NickName = nombreJugador;
                jugador.Score = 0;
                jugador.IdConexion = connectionId;
                jugadoresContectados.Add(jugador);
                _salaConecctionId[connectionId] = nroSala;
                Groups.AddToGroupAsync(connectionId, nroSala.ToString());
                await Clients.Groups(nroSala.ToString()).SendAsync("ActualizarListaJugadores", jugadoresContectados);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            string connectionId = Context.ConnectionId;
            int nroSala = obtenerSalaPorIdConnection(connectionId);
            List<Jugador> jugadoresContectados = obtenerJugadoresSala(nroSala);
            Jugador jugadorAEliminar = jugadoresContectados.FirstOrDefault(jugador => jugador.IdConexion == connectionId);
            jugadoresContectados.Remove(jugadorAEliminar);
            await Clients.Group(nroSala.ToString()).SendAsync("ActualizarListaJugadores", jugadoresContectados);
        }

        public async Task SumarPuntaje(int nroSala)
        {
            string connectionId = Context.ConnectionId;
            List<Jugador> jugadoresContectados = obtenerJugadoresSala(nroSala);
            Jugador jugador = jugadoresContectados.FirstOrDefault(jugador => jugador.IdConexion == connectionId);
            jugador.Score += 3;
            jugadoresContectados.Sort((jugador1, jugador2) => jugador2.Score.CompareTo(jugador1.Score));
            await Clients.Group(nroSala.ToString()).SendAsync("ActualizarListaJugadores", jugadoresContectados);
        }

        public async Task RestarPuntaje(int nroSala)
        {
            string connectionId = Context.ConnectionId;
            List<Jugador> jugadoresContectados = obtenerJugadoresSala(nroSala);
            Jugador jugador = jugadoresContectados.FirstOrDefault(jugador => jugador.IdConexion == connectionId);
            if(jugador.Score > 0)
                jugador.Score --;
            jugadoresContectados.Sort((jugador1, jugador2) => jugador2.Score.CompareTo(jugador1.Score));
            await Clients.Group(nroSala.ToString()).SendAsync("ActualizarListaJugadores", jugadoresContectados);
        }
        private List<Jugador> obtenerJugadoresSala(int nroSala)
        {
            List<Jugador> listaJugadores = new List<Jugador>();
            if (_salaJugadores.ContainsKey(nroSala))
            {
                listaJugadores = _salaJugadores[nroSala];
            }
            else
            {
                _salaJugadores[nroSala] = listaJugadores;
            }
            return listaJugadores;
        }
        private int obtenerSalaPorIdConnection(string connectionId)
        {
            int nroSala;
            nroSala = _salaConecctionId[connectionId];
            return nroSala;
        }
    }
}
