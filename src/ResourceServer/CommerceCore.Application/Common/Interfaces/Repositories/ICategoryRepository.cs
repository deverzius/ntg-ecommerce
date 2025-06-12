using System.Linq.Expressions;
using CommerceCore.Application.Queries.List;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Common;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface ICategoryRepository : IWriteRepository<Category>
{
    Task<PagedResult<Category>> GetPagedResultAsync(GetCategoriesQuery query, CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);
}
