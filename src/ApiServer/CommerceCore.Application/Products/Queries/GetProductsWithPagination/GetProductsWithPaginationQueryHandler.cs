using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Common.Models;
using CommerceCore.Application.Products.Dtos;
using MediatR;

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
        return await _context
            .Products.Select(p => new ProductResponseDto(p))
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
