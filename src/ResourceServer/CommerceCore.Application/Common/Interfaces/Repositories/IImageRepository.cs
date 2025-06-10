using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface IImageRepository
{
    Task AddAsync(AppImage item, CancellationToken cancellationToken);
}
