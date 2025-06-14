namespace CommerceCore.Application.Common.DTOs;

public record CreateProductDTO(
    string Name,
    string Description,
    decimal Price,
    Guid CategoryId,
    ICollection<CreateProductVariantDTO> Variants
);
