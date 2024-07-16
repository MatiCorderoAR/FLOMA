using System;
using System.Collections.Generic;

namespace GestorVentas_FLOMA.Models;

public partial class Fundaxcelular
{
    public int IdFunda { get; set; }

    public int IdCelular { get; set; }

    public int? Costo { get; set; }

    public int? PrecioVenta { get; set; }

    public virtual Celular IdCelularNavigation { get; set; } = null!;

    public virtual Detalleproducto IdFundaNavigation { get; set; } = null!;
}
