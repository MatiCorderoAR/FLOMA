using System;
using System.Collections.Generic;

namespace GestorVentas_FLOMA.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Celular { get; set; }

    public string? Fuente { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
