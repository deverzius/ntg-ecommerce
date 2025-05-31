using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Application.Common.Mappers;

public static class ProductVariantMapper
{
    public static ProductVariantResponse ToDto(this ProductVariant productVariant)
    {
        return new ProductVariantResponse(
            productVariant.Id,
            productVariant.Name,
            productVariant.Value,
            productVariant.StockQuantity,
            productVariant.Product.Id,
            productVariant.Images.Select(i => i.ToDto()).ToList()
        );
    }
}
