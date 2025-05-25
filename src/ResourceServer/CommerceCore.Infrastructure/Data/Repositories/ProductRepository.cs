using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Domain.Entities;
using CommerceCore.Domain.Interfaces.Repositories;
using CommerceCore.Shared.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ProductRepository(IApplicationDbContext dbContext) : IProductRepository
{
    private readonly DbSet<Product> _dbSet = dbContext.Products;

    public async Task<PagedResult<Product>> GetPagedResultAsync(GetProductsOptions options, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Images)
            // .Where(p => !request.CategoryId.HasValue || p.CategoryId == request.CategoryId)
            .PaginateAsync(options.PageNumber, options.PageSize, cancellationToken);
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
