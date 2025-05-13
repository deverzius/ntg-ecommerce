using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappings;

public static class ProductMappingExtensions
{
    public static ProductResponse ToDto(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            CreatedDate = product.CreatedDate,
            UpdatedDate = product.UpdatedDate
        };
    }

    public static ReviewResponse ToDto(this Review review)
    {
        return new ReviewResponse
        {
            Id = review.Id,
            Rating = review.Rating,
            Title = review.Title,
            Comment = review.Comment,
            CreatedDate = review.CreatedDate,
            FullName = review.FullName,
            PhoneNumber = review.PhoneNumber,
            Email = review.Email
        };
    }
}
