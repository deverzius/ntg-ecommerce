using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface IProductVariantRepository : IWriteRepository<ProductVariant>
{
    // Task<PagedResult<Product>> GetPagedResultAsync(GetProductsQuery query, CancellationToken cancellationToken);
    // Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
