using CommerceCore.Application.Brands.Dtos;
using CommerceCore.Application.Common.Mappings;
using MediatR;

namespace CommerceCore.Application.Brands.Queries.GetBrand;

public class GetBrandQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetBrandQuery, BrandResponseDto?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<BrandResponseDto?> Handle(
        GetBrandQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _context.Brands.FindAsync([request.Id], cancellationToken);

        return result?.ToDto();
    }
}
