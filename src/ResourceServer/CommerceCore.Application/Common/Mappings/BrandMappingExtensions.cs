using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappings;

public static class BrandMappingExtensions
{
    public static BrandResponse ToDto(this Brand brand)
    {
        return new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description
        };
    }
}
