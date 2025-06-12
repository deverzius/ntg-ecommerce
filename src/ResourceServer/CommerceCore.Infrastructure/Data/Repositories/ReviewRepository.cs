using System.Linq.Expressions;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data.Repositories;

public class ReviewRepository(IAppDbContext dbContext) : IReviewRepository
{
    private readonly DbSet<Review> _dbSet = dbContext.Reviews;

    public async Task AddAsync(Review item, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(item, cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }
}
