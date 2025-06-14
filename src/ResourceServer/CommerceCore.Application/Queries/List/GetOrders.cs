using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Shared.DTOs.Common;
using MediatR;

namespace CommerceCore.Application.Queries.List;

public class GetOrdersQuery : PagedResultRequest, IRequest<PagedResult<OrderResponse>>
{
}

public class GetOrdersQueryHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrdersQuery, PagedResult<OrderResponse>>
{
    public async Task<PagedResult<OrderResponse>> Handle(
        GetOrdersQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await orderRepository.GetPagedResultAsync(query, cancellationToken);

        return new PagedResult<OrderResponse>(
            result.Items.Select(o => o.ToDto()).ToList(),
            result.PageNumber,
            result.PageSize,
            result.TotalPages
        );
    }
}
