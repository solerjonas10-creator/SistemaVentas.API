using System;
using System.Collections.Generic;

namespace SistemaVentas.API.Models;

public partial class Venta
{
    public int Id { get; set; }

    public int NroVenta { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public string Condicion { get; set; } = null!;

    public int CantCuotas { get; set; } = 1;

    public int IntervaloDias { get; set; } = 0;

    public int IdCliente { get; set; }

    public string Estado { get; set; } = "PENDIENTE";

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
