using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Products.Queries.GetProductsWithPagination;

public class GetProductsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetProductsQuery, PaginatedList<ProductResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedList<ProductResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        // TODO: optimize query
        return await _context
            .Products.AsNoTracking()
            .Where(p => !request.CategoryId.HasValue || p.CategoryId == request.CategoryId)
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Select(p => p.ToDto())
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
