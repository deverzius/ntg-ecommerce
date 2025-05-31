namespace CommerceCore.Shared.DTOs.Create;

public record CreateProductVariantRequest(
    Guid Id,
    string Name,
    string Value,
    int StockQuantity,
    ICollection<Guid> ImageIds
);
