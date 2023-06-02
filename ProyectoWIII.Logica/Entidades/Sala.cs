using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Rompecabezas.Logica.Entidades
{
    public class Sala
    {
        [Required(ErrorMessage = "Por favor Ingrese un nombre de usuario.")]
        public string NombreUsuarioCreador { get; set; }
        [Required(ErrorMessage = "Por favor seleccione la cantidad de piezas.")]
        public  int CantPiezas { get; set; }
        [StringLength(4, ErrorMessage = "El PIN debe tener exactamente 4 caracteres.")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "El PIN debe ser un numero de 4 caracteres.")]
        public string ? Pin { get; set; }
        public Dictionary<String, int>? PuntajeJugadores { get; set; }
        
    }
}
