using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface ICategoryRepository : IWriteRepository<Category>
{
    // Task<PagedResult<Product>> GetPagedResultAsync(GetProductsQuery query, CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
