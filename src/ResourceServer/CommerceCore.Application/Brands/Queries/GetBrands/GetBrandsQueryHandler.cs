using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Application.Common.Models;
using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrands;

public class GetBrandsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBrandsQuery, PaginatedList<BrandResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedList<BrandResponse>> Handle(
        GetBrandsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Brands.Select(p => p.ToDto())
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
