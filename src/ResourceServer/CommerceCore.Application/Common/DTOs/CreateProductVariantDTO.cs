namespace CommerceCore.Application.Common.DTOs;

public record CreateProductVariantDTO(
    string Name,
    string Value,
    string DisplayValue,
    ICollection<Guid> ImageIds
);
