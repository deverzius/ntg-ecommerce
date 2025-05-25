namespace CommerceCore.Application.Common.Interfaces;

public interface IWriteRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity item, CancellationToken cancellationToken);
    void Update(TEntity item);
    void Remove(TEntity item);
}