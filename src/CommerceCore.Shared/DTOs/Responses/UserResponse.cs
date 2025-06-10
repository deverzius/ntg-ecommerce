namespace CommerceCore.Shared.DTOs.Responses;

public record UserResponse(
    string Id,
    string UserName,
    string Email,
    string Role,
    string? PhoneNumber = null
);
