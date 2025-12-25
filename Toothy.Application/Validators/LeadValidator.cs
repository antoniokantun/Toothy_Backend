using FluentValidation;
using Toothy.Application.DTOs.Leads;

namespace Toothy.Application.Validators
{
    public class CreateLeadDtoValidator : AbstractValidator<CreateLeadDto>
    {
        public CreateLeadDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 letras");

            RuleFor(x => x.CorreoElectronico)
                .NotEmpty()
                .EmailAddress().WithMessage("El formato del correo es incorrecto");

            RuleFor(x => x.Telefono)
                .NotEmpty()
                .Matches(@"^\d{10}$").WithMessage("El teléfono debe tener 10 dígitos numéricos");
        }
    }
}
