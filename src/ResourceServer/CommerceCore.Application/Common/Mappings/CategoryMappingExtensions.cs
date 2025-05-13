using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappings;

public static class CategoryMappingExtensions
{
    public static CategoryResponse ToDto(this Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId
        };
    }
}
