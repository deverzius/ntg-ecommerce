using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Shared.DTOs.Common;
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
            result.Items.Select(p => p.ToDto()).ToList(),
            result.PageNumber,
            result.PageSize,
            result.TotalPages
        );
    }
}