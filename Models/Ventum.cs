using System;
using System.Collections.Generic;

namespace GestorVentas_FLOMA.Models;

public partial class Ventum
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int? IdDetalleProducto { get; set; }

    public int? IdCliente { get; set; }

    public int Precio { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string? DomicilioEntrega { get; set; }

    public int? IdDescuento { get; set; }

    public int Cantidad { get; set; }

    public ulong? Complete { get; set; }

    public ulong? Active { get; set; }

    public int? IdVendedor { get; set; }

    public string? Descripcion { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Descuento? IdDescuentoNavigation { get; set; }

    public virtual Detalleproducto? IdDetalleProductoNavigation { get; set; }

    public virtual Vendedor? IdVendedorNavigation { get; set; }
}
