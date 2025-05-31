namespace CommerceCore.Shared.DTOs.Responses;

public record ProductVariantResponse(
    Guid Id,
    string Name,
    string Value,
    int StockQuantity,
    Guid ProductId,
    ICollection<ImageResponse> Images
);
