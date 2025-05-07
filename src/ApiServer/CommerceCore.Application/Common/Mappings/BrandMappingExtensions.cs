using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappings;

public static class BrandMappingExtensions
{
    public static BrandResponseDto ToDto(this Brand brand)
    {
        return new BrandResponseDto
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description,
            Products = brand.Products,
        };
    }

    public static SimpleBrandResponseDto ToSimpleDto(this Brand brand)
    {
        return new SimpleBrandResponseDto
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description,
        };
    }
}
