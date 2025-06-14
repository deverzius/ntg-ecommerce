using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ProductVariantRepository(IAppDbContext dbContext) : IProductVariantRepository
{
    private readonly DbSet<ProductVariant> _dbSet = dbContext.ProductVariants;

    public async Task<ProductVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(pv => pv.Product)
            .FirstOrDefaultAsync(pv => pv.Id == id, cancellationToken);
    }

    public async Task<ICollection<ProductVariant>> GetByIdsAsync(
        ICollection<Guid> ids,
        CancellationToken cancellationToken
    )
    {
        return await _dbSet
            .Include(pv => pv.Product)
            .Where(pv => ids.Contains(pv.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductVariant item, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }

    public void Update(ProductVariant item)
    {
        _dbSet.Entry(item).State = EntityState.Modified;
    }

    public void Remove(ProductVariant item)
    {
        _dbSet.Remove(item);
    }
}
