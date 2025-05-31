namespace CommerceCore.Shared.DTOs.Create;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId,
    ICollection<CreateProductVariantRequest> Variants
);
