using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaVentas.API.Models;

public partial class Venta
{
    public int? Id { get; set; }

    public int NroVenta { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public string Condicion { get; set; } = "CONTADO";

    public int CantCuotas { get; set; } = 1;

    public int IntervaloDias { get; set; } = 0;

    public int IdCliente { get; set; }

    public string Estado { get; set; } = "PENDIENTE";

    [JsonPropertyName("detalleVenta")]
    public virtual ICollection<DetalleVenta> DETALLE_VENTAS { get; set; } = new List<DetalleVenta>();
}
