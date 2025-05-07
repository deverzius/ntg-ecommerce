using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappings;

public static class CategoryMappingExtensions
{
    public static CategoryResponseDto ToDto(this Category category)
    {
        return new CategoryResponseDto()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            ParentCategory = category.ParentCategory,
            Products = category.Products,
        };
    }

    public static SimpleCategoryResponseDto ToSimpleDto(this Category category)
    {
        return new SimpleCategoryResponseDto()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
        };
    }
}
