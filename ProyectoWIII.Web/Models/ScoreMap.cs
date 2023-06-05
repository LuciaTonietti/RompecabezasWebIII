using System;
using System.Collections.Generic;

namespace Rompecabezas.Web.Models;

public partial class ScoreMap
{
    public int Id { get; set; }

    public string? NickName { get; set; }

    public double? Score { get; set; }

    public virtual ICollection<Sala> Salas { get; set; } = new List<Sala>();
}
