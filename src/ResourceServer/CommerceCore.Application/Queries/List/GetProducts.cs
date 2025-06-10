using System.Collections.Immutable;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Shared.DTOs.Common;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Queries.List;

public class GetProductsQuery() : PagedResultRequest, IRequest<PagedResult<ProductResponse>>
{
    public Guid? CategoryId { get; init; }
}

public class GetProductsQueryHandler(IProductRepository repository)
    : IRequestHandler<GetProductsQuery, PagedResult<ProductResponse>>
{
    public async Task<PagedResult<ProductResponse>> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await repository.GetPagedResultAsync(query, default);

        return new PagedResult<ProductResponse>(
            result.Items.Select(p => p.ToDto()).ToImmutableArray(),
            result.PageNumber,
            result.PageSize,
            result.TotalPages
        );
    }
}