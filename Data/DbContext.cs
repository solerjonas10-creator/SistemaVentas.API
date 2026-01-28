using Microsoft.EntityFrameworkCore;
using SistemaVentas.API.Models;

namespace SistemaVentas.API.Data
{
    public class VentasContext : DbContext
    {
        public VentasContext(DbContextOptions<VentasContext> options) : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

        public virtual DbSet<Producto> Productos { get; set; }

        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
