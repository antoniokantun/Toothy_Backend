using FluentValidation;
using Toothy.Application.DTOs.Recepcionistas;

namespace Toothy.Application.Validators
{
    public class CreateRecepcionistaDtoValidator : AbstractValidator<CreateRecepcionistaDto>
    {
        public CreateRecepcionistaDtoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 letras");
            RuleFor(x => x.Apellido).NotEmpty().Length(2, 100).WithMessage("El apellido debe tener entre 2 y 100 letras");

            RuleFor(x => x.CorreoElectronico)
                .NotEmpty()
                .EmailAddress().WithMessage("El formato del correo es inválido.");

            RuleFor(x => x.Telefono)
                .Matches(@"^\d+$").WithMessage("El teléfono solo debe contener números.");
        }
    }
}
