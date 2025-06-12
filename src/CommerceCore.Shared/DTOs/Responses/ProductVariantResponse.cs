namespace CommerceCore.Shared.DTOs.Responses;

public record ProductVariantResponse(
    Guid Id,
    string Name,
    string Value,
    string DisplayValue,
    ICollection<ImageResponse> Images
);
