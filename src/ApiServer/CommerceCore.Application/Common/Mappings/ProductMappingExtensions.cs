using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Common.Models;
using CommerceCore.Application.Products.Dtos;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappings;

public static class ProductMappingExtensions
{
    public static ProductResponseDto ToDto(this Product product)
    {
        return new ProductResponseDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            CreatedDate = product.CreatedDate,
            UpdatedDate = product.UpdatedDate,
            Brand = product.Brand?.ToSimpleDto(),
            Category = product.Category?.ToSimpleDto(),
            Images = [.. product.Images.Select(i => i.ToDto())],
            Variants = product.Variants,
        };
    }

    public static ReviewResponseDto ToDto(this Review review)
    {
        return new ReviewResponseDto()
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

    public static SimpleProductImageResponseDto ToDto(this ProductImage image)
    {
        return new SimpleProductImageResponseDto()
        {
            Name = image.Name,
            Path = image.Path,
            ProductId = image.ProductId,
        };
    }
}
