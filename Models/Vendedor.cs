using System;
using System.Collections.Generic;

namespace GestorVentas_FLOMA.Models;

public partial class Vendedor
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
