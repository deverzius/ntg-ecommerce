namespace CommerceCore.Shared.DTOs.Create;

public record CreateImageRequest(
    string Name,
    string? Url,
    string Type,
    List<string> Tags
);

