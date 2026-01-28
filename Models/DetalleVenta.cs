using System;
using System.Collections.Generic;

namespace SistemaVentas.API.Models;

public partial class DetalleVenta
{
    public decimal IdVenta { get; set; }

    public decimal IdProducto { get; set; }

    public decimal Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
