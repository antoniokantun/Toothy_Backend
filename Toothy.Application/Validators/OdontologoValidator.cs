using FluentValidation;
using Toothy.Application.DTOs.Odontologos;

namespace Toothy.Application.Validators
{
    public class CreateOdontologoDtoValidator : AbstractValidator<CreateOdontologoDto>
    {
        public CreateOdontologoDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 letras");

            RuleFor(x => x.Apellido).NotEmpty().Length(2, 100).WithMessage("El apellido debe tener entre 2 y 100 letras");

            RuleFor(x => x.CorreoElectronico)
                .NotEmpty()
                .EmailAddress().WithMessage("El formato del correo es inválido.");

            RuleFor(x => x.Especialidad)
                .NotEmpty()
                .Must(e => new[] { "Ortodoncia", "General", "Cirujano", "Endodoncia" }.Contains(e))
                .WithMessage("Especialidad no válida. Solo aceptamos: Ortodoncia, General, Cirujano, Endodoncia");

            RuleFor(x => x.NumeroCedulaProfesional)
                .NotEmpty()
                .Length(7, 8).WithMessage("La cédula debe tener 7 u 8 caracteres");
        }
    }
}
