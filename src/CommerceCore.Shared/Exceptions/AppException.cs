namespace CommerceCore.Shared.Exceptions;

public class AppException(int statusCode, string message) : Exception
{
    public int StatusCode { get; init; } = statusCode;
    public new string Message { get; init; } = message;
}
