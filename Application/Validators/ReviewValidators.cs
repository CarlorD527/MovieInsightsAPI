using Application.Dtos.Movie;
using Application.Dtos.Review;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class ReviewValidators : AbstractValidator<InsertReviewDto>
    {
        public ReviewValidators()
        {
            RuleFor(review => review.MovieId)
                .NotEmpty().WithMessage("El campo MovieId es obligatorio.");

            RuleFor(review => review.UserId)
                .NotEmpty().WithMessage("El campo UserId es obligatorio.");

            RuleFor(review => review.ReviewContent)
                .NotEmpty().WithMessage("El campo ReviewContent es obligatorio.")
                .MaximumLength(500).WithMessage("El campo ReviewContent no debe exceder los 500 caracteres.");

            RuleFor(review => review.Score)
                .InclusiveBetween(1, 5).WithMessage("El campo Score debe estar en el rango de 1 a 5.");
        }
    }
}
