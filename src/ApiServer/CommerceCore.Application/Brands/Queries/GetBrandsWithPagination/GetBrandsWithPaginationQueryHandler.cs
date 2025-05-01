using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrandsWithPagination;

public class GetBrandsWithPaginationQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBrandsWithPaginationQuery, PaginatedList<BrandResponseDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedList<BrandResponseDto>> Handle(
        GetBrandsWithPaginationQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Brands.Select(p => new BrandResponseDto(p))
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
