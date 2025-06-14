namespace CommerceCore.Shared.DTOs.Responses;

public record OrderItemResponse(
    int Quantity,
    decimal ProductPrice,
    string ProductName,
    string ProductVariantName,
    string ProductVariantValue,
    Guid ProductVariantId
);
