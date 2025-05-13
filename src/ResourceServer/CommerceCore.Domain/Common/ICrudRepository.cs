namespace CommerceCore.Domain.Common;

public interface ICrudRepository<T, TID> where T : class
{
    IQueryable<T> GetQueryableList();
    Task<T?> GetByIdAsync(TID id, CancellationToken cancellationToken);
    Task AddAsync(T item, CancellationToken cancellationToken);
    void Update(T item);
    void Remove(T item);
}