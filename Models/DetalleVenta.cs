using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVentas.API.Models;

public partial class DetalleVenta
{
    public int? Id { get; set; }
    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; } = 0;

    [ForeignKey("IdVenta")]
    public virtual Venta? Venta { get; set; }
}
