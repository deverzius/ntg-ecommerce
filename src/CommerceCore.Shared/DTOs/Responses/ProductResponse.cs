namespace CommerceCore.Shared.DTOs.Responses;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    CategoryResponse Category,
    ICollection<ProductVariantResponse> ProductVariants
);
