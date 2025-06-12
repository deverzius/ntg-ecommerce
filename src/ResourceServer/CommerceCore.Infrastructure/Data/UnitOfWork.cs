using CommerceCore.Application.Common.Interfaces;

namespace CommerceCore.Infrastructure.Data;

public class UnitOfWork(IAppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
