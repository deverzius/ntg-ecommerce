using FluentValidation;

namespace CommerceCore.Application.Common;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string?> MustBeValidUrl<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));
    }
}