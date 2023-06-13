using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rompecabezas.Logica.Models;

public partial class Sala
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Por favor Ingrese un nombre de usuario.")]
    public string? NickName { get; set; }
    [Required(ErrorMessage = "Por favor seleccione la cantidad de piezas.")]
    public int? CantPieces { get; set; }
    
   public int? LimiteParticipantes { get; set; }

    [StringLength(4, ErrorMessage = "El PIN debe tener exactamente 4 caracteres.")]
    [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "El PIN debe ser un numero de 4 caracteres.")]
    public string? Pin { get; set; }
    public int? NroSala { get; set; }

    public virtual ICollection<ScoreMap> ScoreMaps { get; set; } = new List<ScoreMap>();
}
