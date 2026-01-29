using Microsoft.EntityFrameworkCore;
using SistemaVentas.API.Models;

namespace SistemaVentas.API.Data
{
    public class VentasContext : DbContext
    {
        public VentasContext(DbContextOptions<VentasContext> options) : base(options)
        {
        }

        public virtual DbSet<Cliente> CLIENTES { get; set; }

        public virtual DbSet<DetalleVenta> DETALLE_VENTAS { get; set; }

        public virtual DbSet<Producto> PRODUCTOS { get; set; }

        public virtual DbSet<Venta> VENTAS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(eb =>
            {
                eb.ToTable("PRODUCTOS");
                eb.HasKey(p => p.Id);
                eb.Property(p => p.Id).HasColumnName("ID");
                eb.Property(p => p.Descripcion).HasColumnName("DESCRIPCION");
                eb.Property(p => p.PrecioCompra).HasColumnName("PRECIO_COMPRA");
                eb.Property(p => p.PrecioVenta).HasColumnName("PRECIO_VENTA");
                eb.Property(p => p.Iva).HasColumnName("IVA");
                eb.Property(p => p.Stock).HasColumnName("STOCK");
                eb.Property(p => p.Activo).HasColumnName("ACTIVO");
                eb.Property(p => p.Registrado).HasColumnName("REGISTRADO");
                eb.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Cliente>(eb =>
            {
                eb.ToTable("CLIENTES");
                eb.HasKey(c => c.Id);
                eb.Property(c => c.Id).HasColumnName("ID");
                eb.Property(c => c.Nombres).HasColumnName("NOMBRES");
                eb.Property(c => c.Apellidos).HasColumnName("APELLIDOS");
                eb.Property(c => c.NroDoc).HasColumnName("NRO_DOC");
                eb.Property(c => c.Correo).HasColumnName("CORREO");
                eb.Property(c => c.Telefono).HasColumnName("TELEFONO");
                eb.Property(c => c.Direccion).HasColumnName("DIRECCION");
                eb.Property(c => c.Activo).HasColumnName("ACTIVO");
                eb.Property(c => c.Registrado).HasColumnName("REGISTRADO");
                eb.HasKey(c => c.Id);
            });

            modelBuilder.Entity<Venta>(eb =>
            {
                eb.ToTable("VENTAS");
                eb.HasKey(v => v.Id);
                eb.Property(v => v.Id).HasColumnName("ID");
                eb.Property(v => v.IdCliente).HasColumnName("CLIENTE_ID");
                eb.Property(v => v.NroVenta).HasColumnName("NRO_VENTA");
                eb.Property(v => v.Fecha).HasColumnName("FECHA");
                eb.Property(v => v.Condicion).HasColumnName("CONDICION");
                eb.Property(v => v.CantCuotas).HasColumnName("CANT_CUOTAS");
                eb.Property(v => v.IntervaloDias).HasColumnName("INTERVALO_DIAS");
                eb.Property(v => v.Estado).HasColumnName("ESTADO");
                eb.HasKey(v => v.Id);
            });
        }
    }
}
