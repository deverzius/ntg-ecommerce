using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Interfaces.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(Guid UserId, CancellationToken cancellationToken);
    Task AddAsync(Cart item, CancellationToken cancellationToken);
}
