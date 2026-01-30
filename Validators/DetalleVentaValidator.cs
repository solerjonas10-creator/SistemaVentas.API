using FluentValidation;
using SistemaVentas.API.Models;

namespace SistemaVentas.API.Validators
{
    public class DetalleVentaValidator : AbstractValidator<DetalleVenta>
    {
        public DetalleVentaValidator()
        {
            RuleFor(detalle => detalle.IdProducto)
                .NotEmpty().WithMessage("El campo ProductoId es obligatorio.")
                .GreaterThan(0).WithMessage("El campo ProductoId debe ser mayor a 0.");

            RuleFor(detalle => detalle.Cantidad)
                .GreaterThan(0).WithMessage("El campo Cantidad debe ser mayor a 0.");
        }
    }
}
