using CommerceCore.Application.Categories.Dtos;
using CommerceCore.Application.Common.Models;

namespace CommerceCore.WebApi.Shared.Mappings;

public static class CategoryMappingExtensions
{
    public static SimpleCategoryViewModel ToSimpleViewModel(this SimpleCategoryResponseDto category)
    {
        return new SimpleCategoryViewModel()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
    }

    public static SimpleCategoryViewModel ToViewModel(this CategoryResponseDto category)
    {
        return new SimpleCategoryViewModel()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
        };
    }

    public static PaginatedListViewModel<SimpleCategoryViewModel> ToViewModel(
        this PaginatedList<CategoryResponseDto> categories
    )
    {
        return new PaginatedListViewModel<SimpleCategoryViewModel>()
        {
            Items = [.. categories.Items.Select(p => p.ToViewModel())],
            PageNumber = categories.PageNumber,
            TotalPages = categories.TotalPages,
            TotalCount = categories.TotalCount,
            HasPreviousPage = categories.HasPreviousPage,
            HasNextPage = categories.HasNextPage,
        };
    }
}
