using CommerceCore.Application.Common.Mappings;
using CommerceCore.Domain.Interfaces.Repositories;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<ProductResponse?>;

public class GetProduct(IProductRepository repository)
    : IRequestHandler<GetProductQuery, ProductResponse?>
{
    public async Task<ProductResponse?> Handle(
        GetProductQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await repository.GetByIdAsync(query.Id, cancellationToken);

        return result?.ToDto();
    }
}
