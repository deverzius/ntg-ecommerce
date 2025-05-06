using CommerceCore.Application.Products.Dtos;
using MediatR;

namespace CommerceCore.Application.Products.Queries.GetReviewsByProductId;

public record GetReviewsByProductIdQuery(Guid ProductId)
    : IRequest<ICollection<ReviewResponseDto>> { }
