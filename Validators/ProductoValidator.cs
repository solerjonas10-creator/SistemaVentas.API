using FluentValidation;
using SistemaVentas.API.Models;
using System.Security.Cryptography.X509Certificates;

namespace SistemaVentas.API.Validators
{
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator() {
            List<string> iva = new List<string>() { "0", "5", "10" };

            RuleFor(producto => producto.Descripcion)
                .NotEmpty().WithMessage("El campo Descripcion de producto es obligatorio.");

            RuleFor(producto => producto.PrecioCompra)
                .GreaterThanOrEqualTo(0).When(producto => producto.PrecioCompra.HasValue)
                .WithMessage("El Precio de Compra no puede ser negativo.");

            RuleFor(producto => producto.PrecioVenta).GreaterThan(0).When(producto => producto.PrecioVenta.HasValue)
                .WithMessage("El Precio de Venta debe ser mayor a cero.");

            RuleFor(producto => producto.Iva)
                .NotEmpty().WithMessage("El campo IVA es obligatorio.")
                .Must(Iva => Iva != null && iva.Contains(Iva)).WithMessage("El campo IVA debe ser 0, 5 o 10");

            RuleFor(producto => producto.Stock)
                .GreaterThanOrEqualTo(0).When(producto => producto.Stock.HasValue)
                .WithMessage("El Stock no puede ser negativo.");

            RuleFor(producto => producto.Activo)
                .InclusiveBetween(0, 1).WithMessage("El campo Activo debe ser 0 o 1.");
        }
    }
}
