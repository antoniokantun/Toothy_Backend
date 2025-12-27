using FluentValidation;
using Toothy.Application.DTOs.Usuarios;

namespace Toothy.Application.Validators
{
    public class CreateUsuarioDtoValidator : AbstractValidator<CreateUsuarioDto>
    {
        public CreateUsuarioDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio")
                .Length(3, 20).WithMessage("El usuario debe tener entre 3 y 20 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

            RuleFor(x => x.Rol)
                .IsInEnum().WithMessage("Rol inválido.");
        }
    }
}
