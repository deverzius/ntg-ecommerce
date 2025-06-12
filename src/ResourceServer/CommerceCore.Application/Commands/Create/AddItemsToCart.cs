using CommerceCore.Domain.Entities;
using MediatR;

namespace CommerceCore.Application.Commands.Create;

public record AddItemsToCartCommand(Guid Id, List<CreateCartItemDTO> Items)
    : IRequest<CartResponse>;

public class AddItemsToCartCommandHandler(IApplicationDbContext context)
    : IRequestHandler<AddItemsToCartCommand, CartResponse>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<CartResponse> Handle(
        AddItemsToCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var cart = new Cart
        {
            UserId = request.Id
        };

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync(cancellationToken);

        return cart.ToDto();
    }
}
