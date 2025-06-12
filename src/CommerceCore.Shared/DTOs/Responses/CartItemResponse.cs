namespace CommerceCore.Shared.DTOs.Responses;

public record CartItemResponse(
    int Quantity,
    ProductVariantResponse ProductVariant
);