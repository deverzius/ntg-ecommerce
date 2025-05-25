using CommerceCore.Shared.DTOs.Common;

namespace CommerceCore.Domain.Common;

public interface ICrudRepository<TEntity, TID, TGetAllOptions>
    where TEntity : class
    where TGetAllOptions : IGetAllOptions
{
    Task<PagedResult<TEntity>> GetPagedResultAsync(TGetAllOptions options, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(TID id, CancellationToken cancellationToken);
    Task AddAsync(TEntity item, CancellationToken cancellationToken);
    void Update(TEntity item);
    void Remove(TEntity item);
}