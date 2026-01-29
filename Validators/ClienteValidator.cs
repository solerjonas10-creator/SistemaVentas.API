using FluentValidation;
using SistemaVentas.API.Models;

namespace SistemaVentas.API.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator() 
        { 
            RuleFor(cliente => cliente.Nombres)
                .NotEmpty().WithMessage("El campo Nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El campo Nombre no puede exceder los 100 caracteres.");

            RuleFor(cliente => cliente.Apellidos)
                .MaximumLength(100).WithMessage("El campo Apellido no puede exceder los 100 caracteres.");

            // falta validar que sea unico
            RuleFor(cliente => cliente.NroDoc)
                .NotEmpty().WithMessage("El campo NroDoc es obligatorio.")
                .MaximumLength(20).WithMessage("El campo NroDoc no puede exceder los 20 caracteres.");

            RuleFor(cliente => cliente.Correo)
                .EmailAddress().When(cliente => cliente != null)
                .WithMessage("El campo Correo debe tener un formato Email valido.");

            RuleFor(cliente => cliente.Activo)
                .InclusiveBetween(0, 1).WithMessage("El campo Activo debe ser 0 o 1.");

            RuleFor(cliente => cliente.Direccion)
                .MaximumLength(200).WithMessage("El campo Direccion no puede exceder los 200 caracteres.");

            RuleFor(cliente => cliente.Telefono)
                .MaximumLength(20).WithMessage("El campo Telefono no puede exceder los 20 caracteres.");
        }
    }
}
