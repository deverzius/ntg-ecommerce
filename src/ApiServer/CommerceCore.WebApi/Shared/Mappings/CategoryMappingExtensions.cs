using CommerceCore.Application.Categories.Dtos;
using CommerceCore.SharedViewModels;

namespace CommerceCore.WebApi.Shared.Mappings;

public static class CategoryMappingExtensions
{
    public static SimpleCategoryViewModel ToViewModel(this SimpleCategoryResponseDto category)
    {
        return new SimpleCategoryViewModel()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
    }
}
