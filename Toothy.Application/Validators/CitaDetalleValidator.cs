using FluentValidation;
using Toothy.Application.DTOs.Citas;

namespace Toothy.Application.Validators
{
    public class CreateCitaDetalleDtoValidator : AbstractValidator<CreateCitaDetalleDto>
    {
        public CreateCitaDetalleDtoValidator()
        {
            RuleFor(x => x.TratamientoId)
                .GreaterThan(0).WithMessage("ID de tratamiento inválido.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser al menos 1.")
                .LessThan(100).WithMessage("La cantidad es sospechosamente alta.");

            RuleFor(x => x.Observaciones)
                .MaximumLength(500).WithMessage("Las observaciones no pueden exceder 500 caracteres.");
        }
    }
}
