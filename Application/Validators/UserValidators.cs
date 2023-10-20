using Application.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UserValidators: AbstractValidator<InsertUserDto>
    {
        public UserValidators()
        {
            RuleFor(user => user.Email)
           .NotEmpty().WithMessage("El campo Email es obligatorio.")

           .EmailAddress().WithMessage("El campo Email debe ser una dirección de correo electrónico válida.");
            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("El campo Username es obligatorio.")
                .Length(3, 20).WithMessage("El campo Username debe tener entre 3 y 20 caracteres.")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("El campo Username solo puede contener letras y números.");

            RuleFor(user => user.Password)
           .NotEmpty().WithMessage("El campo Password es obligatorio.")
           .MinimumLength(6).WithMessage("El campo Password debe tener al menos 6 caracteres.");
        }
    }
}
