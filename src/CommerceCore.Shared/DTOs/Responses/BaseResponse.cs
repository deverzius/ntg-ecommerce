namespace CommerceCore.Shared.DTOs.Responses;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }

    public BaseResponse(bool success, string message, T? data = default)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static BaseResponse<T> SuccessResponse(T data, string message = "Request successful") =>
        new BaseResponse<T>(true, message, data);

    public static BaseResponse<T> ErrorResponse(string message) =>
        new BaseResponse<T>(false, message);
}
