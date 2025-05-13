using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Products.Queries.GetReviewsByProductId;

public class GetReviewsByProductIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetReviewsByProductIdQuery, ICollection<ReviewResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ICollection<ReviewResponse>> Handle(
        GetReviewsByProductIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Reviews.Where(r => r.ProductId == request.ProductId)
            .Select(r => r.ToDto())
            .ToListAsync(cancellationToken);
    }
}
