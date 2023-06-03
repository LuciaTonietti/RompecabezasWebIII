using Rompecabezas.Logica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rompecabezas.Logica.Servicios
{
    public interface ISalaService
    {
        public Sala? AgregarSala(Sala sala);
        public Sala? ObtenerSalaPorNumero(int numberSala);
        public Sala? ObtenerSalaPorId(int id);
        Sala ObtenerSala(int nroSala, string? pinIngresado, string? nombreUsuario);
        public bool EstaReptidoElNickName(string? nickName);
    }
}
