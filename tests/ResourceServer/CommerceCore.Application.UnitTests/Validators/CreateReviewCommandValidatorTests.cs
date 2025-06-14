using CommerceCore.Application.Commands.Create;
using CommerceCore.Application.Validators.Create;
using FluentValidation.TestHelper;

namespace CommerceCore.Application.UnitTests.Validators;

public class CreateReviewCommandValidatorTests
{
    [Fact]
    public void ValidCommand_PassesValidation()
    {
        var validator = new CreateReviewCommandValidator();
        var command = new CreateReviewCommand(
            5,
            "Great product",
            "I loved it!",
            Guid.NewGuid(),
            "John Doe",
            "1234567890",
            "user@example.com"
        );

        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void EmptyTitle_FailsValidation()
    {
        var validator = new CreateReviewCommandValidator();
        var command = new CreateReviewCommand(
            4,
            "",
            "Nice",
            Guid.NewGuid(),
            "Jane",
            "1234567890",
            "user@example.com"
        );

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void TitleTooLong_FailsValidation()
    {
        var validator = new CreateReviewCommandValidator();
        var longTitle = new string('a', 101);
        var command = new CreateReviewCommand(
            4,
            longTitle,
            "Nice",
            Guid.NewGuid(),
            "Jane",
            "1234567890",
            "user@example.com"
        );

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void RatingOutOfRange_FailsValidation()
    {
        var validator = new CreateReviewCommandValidator();
        var commandLow = new CreateReviewCommand(
            0,
            "Title",
            "Comment",
            Guid.NewGuid(),
            "Jane",
            "1234567890",
            "user@example.com"
        );
        var commandHigh = new CreateReviewCommand(
            6,
            "Title",
            "Comment",
            Guid.NewGuid(),
            "Jane",
            "1234567890",
            "user@example.com"
        );

        var resultLow = validator.TestValidate(commandLow);
        var resultHigh = validator.TestValidate(commandHigh);

        resultLow.ShouldHaveValidationErrorFor(x => x.Rating);
        resultHigh.ShouldHaveValidationErrorFor(x => x.Rating);
    }

    [Fact]
    public void NullOrLongComment_FailsValidation()
    {
        var validator = new CreateReviewCommandValidator();
        var longComment = new string('b', 401);
        var commandNull = new CreateReviewCommand(
            3,
            "Title",
            null,
            Guid.NewGuid(),
            "Jane",
            "1234567890",
            "user@example.com"
        );
        var commandLong = new CreateReviewCommand(
            3,
            "Title",
            longComment,
            Guid.NewGuid(),
            "Jane",
            "1234567890",
            "user@example.com"
        );

        var resultNull = validator.TestValidate(commandNull);
        var resultLong = validator.TestValidate(commandLong);

        resultNull.ShouldHaveValidationErrorFor(x => x.Comment);
        resultLong.ShouldHaveValidationErrorFor(x => x.Comment);
    }

    [Fact]
    public void InvalidEmail_FailsValidation()
    {
        var validator = new CreateReviewCommandValidator();
        var command = new CreateReviewCommand(
            4,
            "Title",
            "Comment",
            Guid.NewGuid(),
            "Jane",
            "1234567890",
            "not-an-email"
        );

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
}