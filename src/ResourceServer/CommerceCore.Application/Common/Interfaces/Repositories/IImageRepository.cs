using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface IImageRepository
{
    Task AddAsync(Image item, CancellationToken cancellationToken);
}
