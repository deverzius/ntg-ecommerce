using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetProductQuery, ProductResponse?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ProductResponse?> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken
    )
    {
        // TODO: optimize query
        var result = await _context
            .Products.Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        return result?.ToDto();
    }
}
