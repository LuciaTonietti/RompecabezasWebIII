using System;
using System.Collections.Generic;

namespace Rompecabezas.Logica.Models;

public partial class Participante
{
    public int Id { get; set; }

    public string? Nickname { get; set; }

    public int? Sala { get; set; }

    public virtual Sala? SalaNavigation { get; set; }
}
