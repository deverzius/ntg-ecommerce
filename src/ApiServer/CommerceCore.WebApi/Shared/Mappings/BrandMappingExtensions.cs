using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Common.Models;

namespace CommerceCore.WebApi.Shared.Mappings;

public static class BrandMappingExtensions
{
    public static SimpleBrandViewModel ToSimpleViewModel(this BrandResponseDto brand)
    {
        return new SimpleBrandViewModel()
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description,
        };
    }

    public static SimpleBrandViewModel ToSimpleViewModel(this SimpleBrandResponseDto brand)
    {
        return new SimpleBrandViewModel()
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description,
        };
    }

    public static PaginatedListViewModel<SimpleBrandViewModel> ToSimpleViewModel(
        this PaginatedList<BrandResponseDto> brands
    )
    {
        return new PaginatedListViewModel<SimpleBrandViewModel>()
        {
            Items = [.. brands.Items.Select(b => b.ToSimpleViewModel())],
            PageNumber = brands.PageNumber,
            TotalPages = brands.TotalPages,
            TotalCount = brands.TotalCount,
            HasPreviousPage = brands.HasPreviousPage,
            HasNextPage = brands.HasNextPage,
        };
    }
}
