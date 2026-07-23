using EatKath.API.DTOs.Area;
using FluentValidation;

namespace EatKath.API.Validators.Area;

public class UpdateAreaValidator : AbstractValidator<UpdateAreaDto>
{
    public UpdateAreaValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}