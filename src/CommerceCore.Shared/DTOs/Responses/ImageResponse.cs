namespace CommerceCore.Shared.DTOs.Responses;

public record ImageResponse(
    Guid Id,
    string Name,
    string Url,
    string Type,
    DateTime UploadedDate,
    ICollection<string> Tags
);
