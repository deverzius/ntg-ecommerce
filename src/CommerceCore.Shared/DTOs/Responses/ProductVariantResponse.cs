namespace CommerceCore.Shared.DTOs.Responses;

public record ProductVariantResponse(
    Guid Id,
    string Name,
    string Value,
    int StockQuantity,
    ProductResponse Product,
    ICollection<ImageResponse> Images
);
