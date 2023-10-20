using Application.Dtos.Movie;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{

        public class MovieValidators : AbstractValidator<InsertMovieDto>
        {
            public MovieValidators()
            {
                RuleFor(x => x.MovieTitle).NotNull().WithMessage("El campo titulo de la pelicula no puede ser nulo")
                  .NotEmpty().WithMessage("El campo titulo carta no puede estar vacio")
                  .MaximumLength(100).WithMessage("El titulo de la pelicula debe tener menos de 100 caracteres");

                RuleFor(x => x.MovieDescription).NotNull().WithMessage("La descripción de la pelicula no puede ser nulo")
                 .NotEmpty().WithMessage("El contenido de la descripción no puede estar vacio")
                 .MaximumLength(500).WithMessage("El contenido de la pelicula debe tener menos de 500 caracteres");

                RuleFor(x => x.PremiereMonth).LessThanOrEqualTo(12).WithMessage("El mes tiene que ser menor a 12");

                RuleFor(x => x.PremiereYear).InclusiveBetween(1900,2023).WithMessage("El año debe estar entre 1900 y 2023");

                RuleFor(x => x.PremiereDay).InclusiveBetween(1, 31).WithMessage("El dia  debe estar entre 1 y 31");
        }
        }
    
}
