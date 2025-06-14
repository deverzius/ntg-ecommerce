using CommerceCore.Application.Queries.List;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Common;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<PagedResult<Order>> GetPagedResultAsync(GetMyOrdersQuery query, CancellationToken cancellationToken);
    Task<PagedResult<Order>> GetPagedResultAsync(GetOrdersQuery query, CancellationToken cancellationToken);
    Task<Order?> GetMyOrder(GetMyOrderQuery query, CancellationToken cancellationToken);
    Task AddAsync(Order item, CancellationToken cancellationToken);
}
