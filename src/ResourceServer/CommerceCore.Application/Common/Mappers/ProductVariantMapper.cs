using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappers;

public static class ProductVariantMapper
{
    public static ProductVariantResponse ToDto(this ProductVariant variant)
    {
        return new ProductVariantResponse(
            variant.Id,
            variant.Name,
            variant.Value,
            variant.DisplayValue,
            variant.Product.Id,
            variant.Images.Select(i => i.ToDto()).ToList()
        );
    }
}
