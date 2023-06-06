using System;
using System.Collections.Generic;

namespace Rompecabezas.Logica.Models;

public partial class ScoreMap
{
    public int Id { get; set; }

    public string? NickName { get; set; }

    public double? Score { get; set; }

    public int? Sala { get; set; }

    public virtual Sala? SalaNavigation { get; set; }
}
