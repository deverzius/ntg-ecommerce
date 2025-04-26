namespace CommerceCore.Application.Common.Interfaces;

public interface IRequestDto<TEntity> where TEntity : class
{
    public TEntity ToModelInstance();
}