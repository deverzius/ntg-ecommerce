using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Common.DTOs;
using CommerceCore.Application.Validators.Create;
using FluentValidation.TestHelper;

namespace CommerceCore.Application.UnitTests.Validators;

public class CreateProductCommandValidatorTests
{
	[Fact]
	public void ValidCommand_PassesValidation()
	{
		var validator = new CreateProductCommandValidator();
		var productDto = new CreateProductDTO(
			"Phone",
			"Smartphone",
			1000,
			Guid.NewGuid(),
			new List<CreateProductVariantDTO> { new("Color", "Red", "Red", []) }
		);
		var command = new CreateProductCommand(productDto);

		var result = validator.TestValidate(command);

		result.ShouldNotHaveAnyValidationErrors();
	}

	[Fact]
	public void EmptyNameAndDescription_FailsValidation()
	{
		var validator = new CreateProductCommandValidator();
		var productDto = new CreateProductDTO(
			"",
			"",
			0,
			Guid.Empty,
			null
		);
		var command = new CreateProductCommand(productDto);

		var result = validator.TestValidate(command);

		result.ShouldHaveValidationErrorFor(x => x.Product.Name);
		result.ShouldHaveValidationErrorFor(x => x.Product.Price);
		result.ShouldHaveValidationErrorFor(x => x.Product.CategoryId);
		result.ShouldHaveValidationErrorFor(x => x.Product.Variants);
	}

	[Fact]
	public void NameTooLong_FailsValidation()
	{
		var validator = new CreateProductCommandValidator();
		var longName = new string('a', 101);
		var productDto = new CreateProductDTO(
			longName,
			"desc",
			10,
			Guid.NewGuid(),
			new List<CreateProductVariantDTO> { new("Color", "Red", "Red", []) }
		);
		var command = new CreateProductCommand(productDto);

		var result = validator.TestValidate(command);

		result.ShouldHaveValidationErrorFor(x => x.Product.Name);
	}

	[Fact]
	public void DescriptionTooLong_FailsValidation()
	{
		var validator = new CreateProductCommandValidator();
		var longDesc = new string('a', 501);
		var productDto = new CreateProductDTO(
			"Name",
			longDesc,
			10,
			Guid.NewGuid(),
			new List<CreateProductVariantDTO> { new("Color", "Red", "Red", []) }
		);
		var command = new CreateProductCommand(productDto);

		var result = validator.TestValidate(command);

		result.ShouldHaveValidationErrorFor(x => x.Product.Description);
	}

	[Fact]
	public void VariantFields_FailsValidation_WhenEmptyOrTooLong()
	{
		var validator = new CreateProductCommandValidator();
		var longValue = new string('b', 101);
		var variants = new List<CreateProductVariantDTO>
	{
		new("", "", "",[]),
		new(longValue, longValue, longValue, [])
	};
		var productDto = new CreateProductDTO(
			"Name",
			"Desc",
			10,
			Guid.NewGuid(),
			variants
		);
		var command = new CreateProductCommand(productDto);

		var result = validator.TestValidate(command);

		// First variant: all empty
		result.ShouldHaveValidationErrorFor("Product.Variants[0].Name");
		result.ShouldHaveValidationErrorFor("Product.Variants[0].Value");
		result.ShouldHaveValidationErrorFor("Product.Variants[0].DisplayValue");

		// Second variant: all too long
		result.ShouldHaveValidationErrorFor("Product.Variants[1].Name");
		result.ShouldHaveValidationErrorFor("Product.Variants[1].Value");
		result.ShouldHaveValidationErrorFor("Product.Variants[1].DisplayValue");
	}
}
