using System;
using System.Collections.Generic;

namespace GestorVentas_FLOMA.Models;

public partial class Descuento
{
    public int Id { get; set; }

    public decimal? Porcentaje { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
