using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserId(Guid UserId, CancellationToken cancellationToken);
    Task AddAsync(Cart item, CancellationToken cancellationToken);
}
