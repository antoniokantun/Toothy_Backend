using FluentValidation;
using Toothy.Application.DTOs.Tratamientos;

namespace Toothy.Application.Validators
{
    public class CreateTratamientoDtoValidator : AbstractValidator<CreateTratamientoDto>
    {
        public CreateTratamientoDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .Length(3, 100).WithMessage("El nombre debe tener entre 2 y 100 letras");

            RuleFor(x => x.Descripcion)
                .Length(0, 500).WithMessage("La descripción no debe exceder los 500 caracteres");

            RuleFor(x => x.CostoBase)
                .GreaterThan(0).WithMessage("El costo debe ser mayor a 0");
        }
    }
}
