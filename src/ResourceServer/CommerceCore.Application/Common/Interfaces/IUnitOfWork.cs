namespace CommerceCore.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveAsync(CancellationToken cancellationToken);
    void Dispose();
}
