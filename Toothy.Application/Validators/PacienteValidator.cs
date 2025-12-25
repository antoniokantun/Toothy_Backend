using FluentValidation;
using Toothy.Application.DTOs.Pacientes;

namespace Toothy.Application.Validators
{
    public class CreatePacienteDtoValidator : AbstractValidator<CreatePacienteDto>
    {
        public CreatePacienteDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 letras");
            RuleFor(x => x.Apellido).NotEmpty().Length(2, 100).WithMessage("El apellido debe tener entre 2 y 100 letras");

            RuleFor(x => x.CorreoElectronico)
                .NotEmpty()
                .EmailAddress().WithMessage("El formato del correo es inválido.");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty()
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe estar en el pasado.");

            RuleFor(x => x.Telefono)
                .Matches(@"^\d*$").WithMessage("El teléfono solo debe contener números.");

            RuleFor(x => x.Genero)
                .IsInEnum().WithMessage("El género no es válido (0=Masculino, 1=Femenino, 2=Otro).");

            RuleFor(x => x.ContactoEmergenciaNombre).NotEmpty().Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 letras");
            RuleFor(x => x.ContactoEmergenciaTelefono)
                .NotEmpty()
                .Matches(@"^\d+$").WithMessage("El teléfono de emergencia solo debe contener números.");
        }
    }
}
