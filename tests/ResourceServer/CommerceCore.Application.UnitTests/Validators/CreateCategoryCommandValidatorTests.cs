using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Validators.Create;
using FluentValidation.TestHelper;

namespace CommerceCore.Application.UnitTests.Validators;

public class CreateCategoryCommandValidatorTests
{
    [Fact]
    public void ValidCommand_PassesValidation()
    {
        var validator = new CreateCategoryCommandValidator();
        var command = new CreateCategoryCommand("Electronics", "All about electronics", null);

        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void EmptyNameAndDescription_FailsValidation()
    {
        var validator = new CreateCategoryCommandValidator();
        var command = new CreateCategoryCommand("", "", null);

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void NameTooLong_FailsValidation()
    {
        var validator = new CreateCategoryCommandValidator();
        var longName = new string('a', 101);
        var command = new CreateCategoryCommand(longName, "desc", null);

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void DescriptionTooLong_FailsValidation()
    {
        var validator = new CreateCategoryCommandValidator();
        var longDesc = new string('a', 501);
        var command = new CreateCategoryCommand("Name", longDesc, null);

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}