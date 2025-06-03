using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Queries.Get;

public record GetProductQuery(Guid Id) : IRequest<ProductResponse?>;

public class GetProductQueryHandler(IProductRepository repository)
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
