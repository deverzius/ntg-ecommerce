using CommerceCore.Application.Commands.Create;
using FluentValidation;

namespace CommerceCore.Application.Validators.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator()
  {
    RuleFor(x => x.Product.Name)
      .NotNull().NotEmpty()
      .WithMessage("Name is required.")
      .MaximumLength(100)
      .WithMessage("Name must not be over 100 characters.");

    RuleFor(x => x.Product.Description)
      .NotNull()
      .WithMessage("Description is required.")
      .MaximumLength(500)
      .WithMessage("Description must not be over 500 characters.");

    RuleFor(x => x.Product.Price).NotNull().NotEmpty().WithMessage("Price is required.");

    RuleFor(x => x.Product.CategoryId).NotNull().NotEmpty().WithMessage("Category Id is required.");

    RuleFor(x => x.Product.Variants).NotNull().WithMessage("Variants must not be null.");

    RuleForEach(x => x.Product.Variants)
      .ChildRules(variant =>
      {
        variant.RuleFor(v => v.Name)
          .NotNull().NotEmpty()
          .WithMessage("Variant name is required.")
          .MaximumLength(100)
          .WithMessage("Variant name must not be over 100 characters.");

        variant.RuleFor(v => v.Value)
          .NotNull().NotEmpty()
          .WithMessage("Variant value is required.")
          .MaximumLength(100)
          .WithMessage("Variant value must not be over 100 characters.");

        variant.RuleFor(v => v.DisplayValue)
          .NotNull().NotEmpty()
          .WithMessage("Variant display value is required.")
          .MaximumLength(100)
          .WithMessage("Variant display value must not be over 100 characters.");
      });
  }
}

