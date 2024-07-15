using System;
using System.Collections.Generic;

namespace GestorVentas_FLOMA.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Detalleproducto> Detalleproductos { get; set; } = new List<Detalleproducto>();
}
