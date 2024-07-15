using System;
using System.Collections.Generic;

namespace GestorVentas_FLOMA.Models;

public partial class Detalleproducto
{
    public int IdDetalle { get; set; }

    public int IdProducto { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Costo { get; set; }

    public int? PrecioVenta { get; set; }

    public byte[]? Img { get; set; }

    public ulong Active { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
