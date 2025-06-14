namespace CommerceCore.Application.Common.DTOs;

public record CreateImageDTO(
    string Name,
    string? Url,
    string Type,
    List<string> Tags
);

