using EatKath.API.DTOs.DiningType;
using FluentValidation;

namespace EatKath.API.Validators.DiningType
{
    public class UpdateDiningTypeValidator : AbstractValidator<UpdateDiningTypeDto>
    {
        public UpdateDiningTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Dining type name is required.")
                .MaximumLength(100);
        }
    }
}