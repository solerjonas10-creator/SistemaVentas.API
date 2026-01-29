namespace SistemaVentas.API.Models.DTOs
{
    public class UsuarioDTO
    {
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!; 
        public string? Rol { get; set; } = "USER";
        public string? NroDoc { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Activo { get; set; } = 1;
    }
}
