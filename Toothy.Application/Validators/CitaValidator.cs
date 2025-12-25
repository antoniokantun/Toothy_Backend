using FluentValidation;
using Toothy.Application.DTOs.Citas;

namespace Toothy.Application.Validators
{
    public class CreateCitaDtoValidator : AbstractValidator<CreateCitaDto>
    {
        public CreateCitaDtoValidator()
        {
            RuleFor(x => x.PacienteId)
                .GreaterThan(0).WithMessage("Debes especificar un Paciente válido.");

            RuleFor(x => x.OdontologoId)
                .GreaterThan(0).WithMessage("Debes especificar un Odontólogo válido.");

            RuleFor(x => x.FechaHoraInicio)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de la cita debe ser en el futuro.");

            RuleFor(x => x.Tratamientos)
                .NotNull().WithMessage("La lista de tratamientos no puede ser nula.")
                .Must(t => t.Count > 0).WithMessage("Debes agregar al menos un tratamiento o servicio a la cita.");

            RuleForEach(x => x.Tratamientos).SetValidator(new CreateCitaDetalleDtoValidator());
        }
    }
}
