using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class CartRepository(IAppDbContext dbContext) : ICartRepository
{
    private readonly DbSet<Cart> _dbSet = dbContext.Carts;

    public async Task<Cart?> GetCartByUserIdAsync(Guid UserId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.ProductVariant)
            .ThenInclude(pv => pv.Product)
            .AsSplitQuery()
            .FirstOrDefaultAsync(
                c => c.UserId == UserId,
                cancellationToken
            );
    }

    public async Task AddAsync(Cart item, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }
}
