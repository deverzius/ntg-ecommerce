using System.Linq.Expressions;
using CommerceCore.Application.Queries.List;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Common;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface IProductRepository : IWriteRepository<Product>
{
    Task<PagedResult<Product>> GetPagedResultAsync(GetProductsQuery query, CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken);
}
