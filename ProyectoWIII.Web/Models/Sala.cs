using System;
using System.Collections.Generic;

namespace Rompecabezas.Web.Models;

public partial class Sala
{
    public int Id { get; set; }

    public string? NickName { get; set; }

    public int? CantPieces { get; set; }

    public string? Pin { get; set; }

    public int? ScoreMap { get; set; }

    public int? NroSala { get; set; }

    public virtual ScoreMap? ScoreMapNavigation { get; set; }
}
