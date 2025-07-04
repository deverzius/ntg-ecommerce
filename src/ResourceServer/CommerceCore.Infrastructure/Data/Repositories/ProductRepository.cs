using System.Linq.Expressions;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Application.Queries.List;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ProductRepository(IAppDbContext dbContext) : IProductRepository
{
    private readonly DbSet<Product> _dbSet = dbContext.Products;

    public async Task<PagedResult<Product>> GetPagedResultAsync(GetProductsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .AsNoTracking()
            .AsSplitQuery()
            .SortBy(query.Sort)
            .SearchBy(query.Search)
            .FilterBy(query.CategoryId)
            .PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Variants)
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

    public async Task<bool> AnyAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }
}
