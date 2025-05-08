using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Common.Models;
using CommerceCore.Application.Products.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductResponseDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedList<ProductResponseDto>> Handle(
        GetProductsWithPaginationQuery request,
        CancellationToken cancellationToken
    )
    {
        // TODO: optimize query
        return await _context
            .Products.AsNoTracking()
            .Where(p => (!request.CategoryId.HasValue) || p.CategoryId == request.CategoryId)
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Select(p => p.ToDto())
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
