using CommerceCore.Application.Common.Interfaces.Repositories;
using MediatR;

namespace CommerceCore.Application.Queries.Get;

public record GetMyCartQuery(Guid UserId) : IRequest<CartResponse?>;

public class GetMyCartQueryHandler(ICartRepository repository)
    : IRequestHandler<GetMyCartQuery, CartResponse?>
{
    public async Task<CartResponse?> Handle(
        GetMyCartQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await repository.GetCartByUserId(query.UserId, cancellationToken);

        return result?.ToDto();
    }
}
