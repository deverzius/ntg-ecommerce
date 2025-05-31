using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Repositories;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class CategoryRepository(IApplicationDbContext dbContext) : ICategoryRepository
{
    private readonly DbSet<Category> _dbSet = dbContext.Categories;

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
}
