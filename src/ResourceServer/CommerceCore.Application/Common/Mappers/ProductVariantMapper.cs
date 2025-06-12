using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Application.Common.Mappers;

public static class ProductVariantMapper
{
    public static ProductVariantResponse ToDto(this ProductVariant variant)
    {
        return new ProductVariantResponse(
            variant.Id,
            variant.Name,
            variant.Value,
            variant.StockQuantity,
            variant.Product.ToDto(),
            variant.Images.Select(i => i.ToDto()).ToList()
        );
    }
}
