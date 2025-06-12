using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class CartRepository(IApplicationDbContext dbContext) : ICartRepository
{
    private readonly DbSet<Cart> _dbSet = dbContext.Carts;

    public async Task<Cart?> GetCartByUserId(Guid UserId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.ProductVariant)
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
