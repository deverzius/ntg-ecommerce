namespace CommerceCore.Shared.DTOs.Requests;

public record BaseRequest(
    int PageNumber,
    int PageSize,
    string Sort
);
