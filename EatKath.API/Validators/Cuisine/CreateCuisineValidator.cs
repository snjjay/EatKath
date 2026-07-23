using EatKath.API.DTOs.Cuisine;
using FluentValidation;

namespace EatKath.API.Validators.Cuisine
{
    public class CreateCuisineValidator : AbstractValidator<CreateCuisineDto>
    {
        public CreateCuisineValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Cuisine name is required.")
                .MaximumLength(100);
        }
    }
}