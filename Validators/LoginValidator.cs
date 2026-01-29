using FluentValidation;
using SistemaVentas.API.Models.DTOs;

namespace SistemaVentas.API.Validators
{
    public class LoginValidator : AbstractValidator<UsuarioDTO>
    {
        public LoginValidator() 
        { 
            RuleFor(login => login.Correo)
                .NotEmpty().WithMessage("El campo Correo es obligatorio.")
                .EmailAddress().WithMessage("El campo Correo debe tener un formato Email valido.")
                .MaximumLength(100).WithMessage("El campo Correo no puede exceder los 100 caracteres.");

            RuleFor(login => login.Clave)
                .NotEmpty().WithMessage("El campo Clave es obligatorio.")
                .MinimumLength(5).WithMessage("El campo Clave debe tener al menos 5 caracteres.");
        }
    }
}
