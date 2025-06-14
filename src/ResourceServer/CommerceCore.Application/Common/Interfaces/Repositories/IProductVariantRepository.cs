using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface IProductVariantRepository : IWriteRepository<ProductVariant>
{
    Task<ProductVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ICollection<ProductVariant>> GetByIdsAsync(ICollection<Guid> id, CancellationToken cancellationToken);
}
