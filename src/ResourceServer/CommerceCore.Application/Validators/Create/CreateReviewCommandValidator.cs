using CommerceCore.Application.Commands.Create;
using FluentValidation;

namespace CommerceCore.Application.Validators.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage("Title must not be over 100 characters.");

        RuleFor(x => x.Rating)
            .LessThanOrEqualTo(5)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Rating must between 1 and 5.");

        RuleFor(x => x.Comment)
            .NotNull()
            .WithMessage("Comment is required.")
            .MaximumLength(400)
            .WithMessage("Comment must not be over 400 characters.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email is not valid.");
    }
}