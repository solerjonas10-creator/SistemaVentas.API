using System;
using System.Collections.Generic;

namespace SistemaVentas.API.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string? Apellidos { get; set; }

    public string NroDoc { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public int Activo { get; set; } = 1;

    public DateTime? Registrado { get; set; } = DateTime.Now;
}
