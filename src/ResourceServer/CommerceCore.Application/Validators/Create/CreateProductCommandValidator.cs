using CommerceCore.Application.Commands.Create;
using FluentValidation;

namespace CommerceCore.Application.Validators.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator()
  {
    RuleFor(x => x.Product.Name)
      .NotNull().NotEmpty()
      .WithMessage("Name is required")
      .MaximumLength(100)
      .WithMessage("Name must not be over 100 characters");

    RuleFor(x => x.Product.Description)
      .NotNull()
      .WithMessage("Description is required")
      .MaximumLength(500)
      .WithMessage("Description must not be over 500 characters");

    RuleFor(x => x.Product.Price).NotNull().NotEmpty().WithMessage("Price is required");

    RuleFor(x => x.Product.CategoryId).NotNull().NotEmpty().WithMessage("Category Id is required");

    RuleFor(x => x.Product.Variants).NotNull().WithMessage("Variants must not be null");
  }
}

