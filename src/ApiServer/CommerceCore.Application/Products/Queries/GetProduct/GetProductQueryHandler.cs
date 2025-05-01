using CommerceCore.Application.Products.Dtos;
using MediatR;

namespace CommerceCore.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetProductQuery, ProductResponseDto?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ProductResponseDto?> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _context.Products.FindAsync([request.Id], cancellationToken);

        return result == null ? null : new ProductResponseDto(result);
    }
}
