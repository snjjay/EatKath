using EatKath.API.DTOs.Area;
using FluentValidation;

namespace EatKath.API.Validators.Area;

public class CreateAreaValidator : AbstractValidator<CreateAreaDto>
{
    public CreateAreaValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Area name is required.")
            .MaximumLength(100).WithMessage("Area name cannot exceed 100 characters.");
    }
}