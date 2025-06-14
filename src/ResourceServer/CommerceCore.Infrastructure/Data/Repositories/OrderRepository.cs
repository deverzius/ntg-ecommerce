using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Application.Queries.List;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class OrderRepository(IAppDbContext dbContext) : IOrderRepository
{
    private readonly DbSet<Order> _dbSet = dbContext.Orders;

    public async Task<PagedResult<Order>> GetPagedResultAsync(GetMyOrdersQuery query, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(o => o.UserId == query.UserId)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.CurrentProductVariant)
            .AsNoTracking()
            .AsSplitQuery()
            .SortBy(query.Sort)
            .SearchBy(query.Search)
            .FilterBy()
            .PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);
    }

    public async Task<PagedResult<Order>> GetPagedResultAsync(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.CurrentProductVariant)
            .AsNoTracking()
            .AsSplitQuery()
            .SortBy(query.Sort)
            .SearchBy(query.Search)
            .FilterBy()
            .PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);
    }

    public async Task<Order?> GetMyOrder(GetMyOrderQuery query, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(o => o.UserId == query.UserId)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.CurrentProductVariant)
            .FirstOrDefaultAsync(oi => oi.Id == query.OrderId, cancellationToken);
    }


    public async Task AddAsync(Order item, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }
}
