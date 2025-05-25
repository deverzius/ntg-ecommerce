using System.Collections.Immutable;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Application.Common.Mappings;
using CommerceCore.Domain.Interfaces.Repositories;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Products.Queries.GetProducts;

public record GetProductsQuery(
    GetProductsOptions Options
) : IRequest<PagedResult<ProductResponse>>;

public class GetProducts(IProductRepository repository)
    : IRequestHandler<GetProductsQuery, PagedResult<ProductResponse>>
{
    public async Task<PagedResult<ProductResponse>> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await repository.GetPagedResultAsync(query.Options, default);

        return new PagedResult<ProductResponse>(
            result.Items.Select(p => p.ToDto()).ToImmutableArray(),
            result.PageNumber,
            result.PageSize,
            result.TotalPages
        );
    }
}
