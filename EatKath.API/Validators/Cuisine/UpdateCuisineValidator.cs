using EatKath.API.DTOs.Cuisine;
using FluentValidation;

namespace EatKath.API.Validators.Cuisine
{
    public class UpdateCuisineValidator : AbstractValidator<UpdateCuisineDto>
    {
        public UpdateCuisineValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Cuisine name is required.")
                .MaximumLength(100);
        }
    }
}