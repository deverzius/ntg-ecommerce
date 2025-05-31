using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Shared.DTOs.Create;

public record CreateProductVariantRequest(
    string Name,
    string Value,
    int StockQuantity,
    ICollection<Guid> ImageIds
);
