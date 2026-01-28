using System;
using System.Collections.Generic;

namespace SistemaVentas.API.Models;

public partial class Cliente
{
    public decimal Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string? Apellidos { get; set; }

    public string NroDoc { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public decimal Activo { get; set; }

    public DateTime? Registrado { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
