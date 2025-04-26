namespace CommerceCore.Application.Common.Interfaces;

public interface ISingleModelService<TEntity, TKey> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<TEntity?> CreateAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TKey id, TEntity entity);
    Task<bool> DeleteAsync(TKey id);
}