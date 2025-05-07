using CommerceCore.Application.Common.Models;
using CommerceCore.Application.Products.Dtos;

namespace CommerceCore.WebApi.Shared.Mappings;

public static class ProductMappingExtensions
{
    public static ProductViewModel ToViewModel(
        this ProductResponseDto product,
        string publicStorageUrl
    )
    {
        return new ProductViewModel()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            CreatedDate = product.CreatedDate,
            UpdatedDate = product.UpdatedDate,
            Brand = product.Brand != null ? product.Brand?.ToViewModel() : null,
            Category = product.Category?.ToViewModel(),
            Images = [.. product.Images.Select(i => i.ToViewModel(publicStorageUrl))],
        };
    }

    public static ReviewViewModel ToViewModel(this ReviewResponseDto review)
    {
        return new ReviewViewModel()
        {
            Id = review.Id,
            Rating = review.Rating,
            Title = review.Title,
            Comment = review.Comment,
            CreatedDate = review.CreatedDate,
            FullName = review.FullName,
            PhoneNumber = review.PhoneNumber,
            Email = review.Email,
        };
    }

    public static PaginatedListViewModel<ProductViewModel> ToViewModel(
        this PaginatedList<ProductResponseDto> products,
        string publicStorageUrl
    )
    {
        return new PaginatedListViewModel<ProductViewModel>()
        {
            Items = [.. products.Items.Select(p => p.ToViewModel(publicStorageUrl))],
            PageNumber = products.PageNumber,
            TotalPages = products.TotalPages,
            TotalCount = products.TotalCount,
            HasPreviousPage = products.HasPreviousPage,
            HasNextPage = products.HasNextPage,
        };
    }

    public static SimpleProductImageViewModel ToViewModel(
        this SimpleProductImageResponseDto image,
        string publicStorageUrl
    )
    {
        return new SimpleProductImageViewModel()
        {
            Name = image.Name,
            PublicUrl = publicStorageUrl + image.Path,
            ProductId = image.ProductId,
        };
    }
}
