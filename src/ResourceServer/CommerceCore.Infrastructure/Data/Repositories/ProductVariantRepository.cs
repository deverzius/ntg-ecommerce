using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Repositories;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ProductVariantRepository(IApplicationDbContext dbContext) : IProductVariantRepository
{
    private readonly DbSet<ProductVariant> _dbSet = dbContext.ProductVariants;

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
