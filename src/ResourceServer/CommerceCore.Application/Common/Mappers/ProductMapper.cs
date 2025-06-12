using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToDto(this Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.CreatedDate,
            product.UpdatedDate,
            product.Category.ToDto(),
            product.Variants.Select(pv => pv.ToDto()).ToList()
        );
    }

    public static ReviewResponse ToDto(this Review review)
    {
        return new ReviewResponse(
            review.Id,
            review.Rating,
            review.Title,
            review.Comment,
            review.CreatedDate,
            review.FullName,
            review.PhoneNumber,
            review.Email
        );
    }
}
