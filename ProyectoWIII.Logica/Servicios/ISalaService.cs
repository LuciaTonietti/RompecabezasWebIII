using Rompecabezas.Logica.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rompecabezas.Logica.Servicios
{
    public interface ISalaService
    {
        public int Crear(Sala sala);
        Sala ObtenerSala(int nroSala);
    }
}
