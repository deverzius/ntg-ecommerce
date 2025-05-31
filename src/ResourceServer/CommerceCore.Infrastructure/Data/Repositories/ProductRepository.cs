using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Application.Common.Repositories;
using CommerceCore.Application.Products.Queries.GetProducts;
using CommerceCore.Domain.Entities;
using CommerceCore.Infrastructure.Extensions;
using CommerceCore.Shared.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ProductRepository(IApplicationDbContext dbContext) : IProductRepository
{
    private readonly DbSet<Product> _dbSet = dbContext.Products;

    public async Task<PagedResult<Product>> GetPagedResultAsync(GetProductsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Images)
            .SortBy(query.Sort)
            .SearchBy(query.Search)
            .FilterBy(query.CategoryId)
            .PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
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
