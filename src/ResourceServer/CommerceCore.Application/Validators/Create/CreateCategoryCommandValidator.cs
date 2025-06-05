using CommerceCore.Application.Commands.Create;
using FluentValidation;

namespace CommerceCore.Application.Validators.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name must not be over 100 characters");

        RuleFor(x => x.Description)
            .NotNull()
            .WithMessage("Description is required")
            .MaximumLength(500)
            .WithMessage("Description must not be over 500 characters");
    }
}