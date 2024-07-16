using System;
using System.Collections.Generic;

namespace GestorVentas_FLOMA.Models;

public partial class Celular
{
    public int IdCelular { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public virtual ICollection<Fundaxcelular> Fundaxcelulars { get; set; } = new List<Fundaxcelular>();
}
