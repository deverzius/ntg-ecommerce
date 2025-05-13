using System.Linq.Expressions;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Common;
using CommerceCore.Domain.Entities;
using CommerceCore.Domain.Interfaces.Repositories;
using CommerceCore.Shared.DTOs.Requests;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ProductRepository(IApplicationDbContext dbContext) : IProductRepository
{
    private readonly DbSet<Product> _dbSet = dbContext.Products;

    public IQueryable<Product> GetQueryableList()
    {
        return _dbSet
            .AsNoTracking()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Images);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Product item, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }

    public void Update(Product item)
    {
        _dbSet.Entry(item).State = EntityState.Modified;
    }

    public void Remove(Product item)
    {
        _dbSet.Remove(item);
    }
}
