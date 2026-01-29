using FluentValidation;
using SistemaVentas.API.Models;

namespace SistemaVentas.API.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(usuario => usuario.NombreUsuario)
            .NotEmpty().WithMessage("El campo NombreUsuario es obligatorio.")
            .MaximumLength(50).WithMessage("El campo NombreUsuario no puede exceder los 60 caracteres.");

            RuleFor(usuario => usuario.Correo)
                .NotEmpty().WithMessage("El campo Correo es obligatorio.")
                .EmailAddress().WithMessage("El campo Correo debe tener un formato Email valido.")
                .MaximumLength(100).WithMessage("El campo Correo no puede exceder los 100 caracteres.");

            RuleFor(usuario => usuario.Clave)
                .NotEmpty().WithMessage("El campo Clave es obligatorio.")
                .MinimumLength(5).WithMessage("El campo Contraseña debe tener al menos 5 caracteres.");

            RuleFor(usuario => usuario.Rol)
                .MaximumLength(100)
                .WithMessage("El campo Rol no puede exceder los 100 caracteres.");

            RuleFor(usuario => usuario.NroDoc)
                .MaximumLength(15)
                .WithMessage("El campo NroDoc no puede exceder los 15 caracteres.");

            RuleFor(usuario => usuario.Telefono)
                .MaximumLength(30)
                .WithMessage("El campo Telefono no puede exceder los 30 caracteres.");

            RuleFor(usuario => usuario.Direccion)
                .MaximumLength(100)
                .WithMessage("El campo Direccion no puede exceder los 100 caracteres.");

            RuleFor(usuario => usuario.FechaNacimiento)
                .LessThanOrEqualTo(DateTime.Now.AddYears(18))
                .WithMessage("El usuario debe ser mayor a 18 Años.");

            RuleFor(usuario => usuario.Activo)
                .InclusiveBetween(0, 1).WithMessage("El campo Activo debe ser 0 o 1.");
        }
    }
}
