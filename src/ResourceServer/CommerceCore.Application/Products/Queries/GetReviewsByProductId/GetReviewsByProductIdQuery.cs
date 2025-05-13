using MediatR;

namespace CommerceCore.Application.Products.Queries.GetReviewsByProductId;

public record GetReviewsByProductIdQuery(Guid ProductId)
    : IRequest<ICollection<ReviewResponse>>
{
}
