using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrand;

public record GetBrandQuery(Guid Id) : IRequest<BrandResponse?>;

public class GetBrandQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBrandQuery, BrandResponse?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<BrandResponse?> Handle(
        GetBrandQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _context.Brands.FindAsync([request.Id], cancellationToken);

        return result?.ToDto();
    }
}
