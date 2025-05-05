using CommerceCore.Application.Brands.Dtos;
using CommerceCore.SharedViewModels;

namespace CommerceCore.WebApi.Shared.Mappings;

public static class BrandMappingExtensions
{
    public static SimpleBrandViewModel ToViewModel(this SimpleBrandResponseDto brand)
    {
        return new SimpleBrandViewModel()
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description,
        };
    }
}
