using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Responses;

namespace CommerceCore.Application.Common.Mappings;

public static class CategoryMappingExtensions
{
    public static CategoryResponse ToDto(this Category category)
    {
        return new CategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.ParentCategoryId
        );
    }
}