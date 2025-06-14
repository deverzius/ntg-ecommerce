using CommerceCore.Application.Common.Interfaces.Repositories;
using MediatR;

namespace CommerceCore.Application.Queries.List;

public record GetMyOrderQuery(
    Guid UserId,
    Guid OrderId
)
: IRequest<OrderResponse?>;

public class GetMyOrderQueryHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetMyOrderQuery, OrderResponse?>
{
    public async Task<OrderResponse?> Handle(
        GetMyOrderQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await orderRepository.GetMyOrder(query, cancellationToken);

        return result?.ToDto();
    }
}
