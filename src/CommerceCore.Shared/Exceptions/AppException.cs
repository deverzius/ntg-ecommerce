namespace CommerceCore.Shared.Exceptions;

public class AppException(int statusCode, string message, string? details = null) : Exception
{
}
