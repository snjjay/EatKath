using EatKath.API.DTOs.DiningType;
using FluentValidation;

namespace EatKath.API.Validators.DiningType
{
    public class CreateDiningTypeValidator : AbstractValidator<CreateDiningTypeDto>
    {
        public CreateDiningTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Dining type name is required.")
                .MaximumLength(100);
        }
    }
}