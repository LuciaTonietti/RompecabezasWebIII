using Rompecabezas.Logica.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rompecabezas.Logica.Servicios
{
    public class SalaService : ISalaService
    {
        public int Crear(Sala sala)
        {
            /*
             1. Llamar al repositorio para crear el registro la BBDD
             2. Retornar el id generado.
            Se hardcodea 1 como para retornar un valor.
             */
            return 1;
        }

        public Sala ObtenerSala(int nroSala)
        {
            /*
             Obtener la sala del repositorio
            Se hardcodea una sala hasta que este el repositorio.
             */
            Sala sala = new Sala() { CantPiezas = 9 , NombreUsuarioCreador = "Rodrigo"};
            return sala;
        }
    }
}
