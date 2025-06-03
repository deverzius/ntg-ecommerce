using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Common;
using FluentValidation;

namespace CommerceCore.Application.Validators.Create;

public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
{
  public CreateImageCommandValidator()
  {
    RuleFor(x => x.Image.Name)
      .NotNull().NotEmpty()
      .WithMessage("Name is required")
      .MaximumLength(100)
      .WithMessage("Name must not be over 100 characters");

    RuleFor(x => x.Image.Url)
      .MustBeValidUrl().When(x => x.Image.Url is not null)
      .WithMessage("Url is not valid")
      .MaximumLength(100)
      .WithMessage("Url must not be over 100 characters");

    RuleFor(x => x.Image.Type).NotNull().NotEmpty().WithMessage("Type is required");

    RuleFor(x => x.Image.Tags).NotNull().WithMessage("Tags must not be null");
  }
}
