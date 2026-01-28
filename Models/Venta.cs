using System;
using System.Collections.Generic;

namespace SistemaVentas.API.Models;

public partial class Venta
{
    public decimal Id { get; set; }

    public decimal NroVenta { get; set; }

    public DateTime Fecha { get; set; }

    public string Condicion { get; set; } = null!;

    public decimal CantCuotas { get; set; }

    public decimal IntervaloDias { get; set; }

    public decimal IdCliente { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
