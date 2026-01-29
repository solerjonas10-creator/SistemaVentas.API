using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVentas.API.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal? PrecioCompra { get; set; } = 0;

    public decimal? PrecioVenta { get; set; }

    public string? Iva { get; set; } = "10";

    /// AGREGADO DE MANERA PRELIMINAR HASTA TENER TABLA ALMACENES Y PODER CREAR UNA TABLA COMPUESTA
    public decimal? Stock { get; set; } = 0;

    public int Activo { get; set; } = 1;

    public DateTime? Registrado { get; set; } = DateTime.Now;
}
