using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrands;

public record GetBrandsQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<PaginatedResponse<BrandResponse>>;

public class GetBrandsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBrandsQuery, PaginatedResponse<BrandResponse>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedResponse<BrandResponse>> Handle(
        GetBrandsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Brands.Select(p => p.ToDto())
            .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
