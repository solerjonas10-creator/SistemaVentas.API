using System;
using System.Collections.Generic;

namespace SistemaVentas.API.Models;

public partial class Producto
{
    public decimal Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal? PrecioCompra { get; set; }

    public decimal? PrecioVenta { get; set; }

    public string? Iva { get; set; }

    /// AGREGADO DE MANERA PRELIMINAR HASTA TENER TABLA ALMACENES Y PODER CREAR UNA TABLA COMPUESTA
    public decimal? Stock { get; set; }

    public decimal Activo { get; set; }

    public DateTime? Registrado { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
}
