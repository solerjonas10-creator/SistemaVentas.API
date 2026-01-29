using FluentValidation;
using SistemaVentas.API.Models;

namespace SistemaVentas.API.Validators
{
    public class VentaValidator : AbstractValidator<Venta>
    {
        public VentaValidator() 
        { 
            RuleFor(venta => venta.Condicion)
                .NotEmpty().WithMessage("El campo Condicion es obligatorio.")
                .Must(condicion => condicion == "CONTADO" || condicion == "CREDITO")
                .WithMessage("El campo Condicion debe ser 'CONTADO' o 'CREDITO'.");

            RuleFor(venta => venta.CantCuotas)
                .GreaterThan(0).WithMessage("El campo CantCuotas debe ser mayor a 0.")
                .When(venta => venta.Condicion == "CREDITO");

            RuleFor(venta => venta.IntervaloDias)
                .GreaterThanOrEqualTo(0).WithMessage("El campo IntervaloDias no puede ser negativo.")
                .When(venta => venta.Condicion == "CREDITO");

            RuleFor(venta => venta.Estado)
                .NotEmpty().WithMessage("El campo Estado es obligatorio.")
                .Must(estado => estado == "PENDIENTE" || estado == "CANCELADO" || estado == "ANULADO")
                .WithMessage("El campo Estado debe ser 'PENDIENTE', 'CANCELADO' o 'ANULADO'.");
        }
    }
}
