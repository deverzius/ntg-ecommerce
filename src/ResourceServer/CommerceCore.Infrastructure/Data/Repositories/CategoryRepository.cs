using System.Linq.Expressions;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Application.Queries.List;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class CategoryRepository(IAppDbContext dbContext) : ICategoryRepository
{
    private readonly DbSet<Category> _dbSet = dbContext.Categories;

    public async Task<PagedResult<Category>> GetPagedResultAsync(GetCategoriesQuery query, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Image)
            .AsNoTracking()
            .AsSplitQuery()
            .SortBy(query.Sort)
            .SearchBy(query.Search)
            .PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Image)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Category item, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }

    public void Update(Category item)
    {
        _dbSet.Entry(item).State = EntityState.Modified;
    }

    public void Remove(Category item)
    {
        _dbSet.Remove(item);
    }

    public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }
}
