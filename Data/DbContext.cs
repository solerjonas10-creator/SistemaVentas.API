using Microsoft.EntityFrameworkCore;

namespace SistemaVentas.API.Data
{
    public class VentasContext : DbContext
    {
        public VentasContext(DbContextOptions<VentasContext> options) : base(options)
        {
        }

        // public DbSet<Producto> Productos { get; set; }
    }
}
