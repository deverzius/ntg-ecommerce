using System.Linq.Expressions;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface IReviewRepository
{
    Task AddAsync(Review item, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken);
}
