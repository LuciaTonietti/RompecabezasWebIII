using System;
using System.Collections.Generic;

namespace Rompecabezas.Logica.Models;

public partial class Sala
{
    public int Id { get; set; }

    public string? NickName { get; set; }

    public int? CantPieces { get; set; }

    public int? LimiteParticipantes { get; set; }

    public string? Pin { get; set; }

    public int? NroSala { get; set; }

    public virtual ICollection<ScoreMap> ScoreMaps { get; set; } = new List<ScoreMap>();
}
