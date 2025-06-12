using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Mappers;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.DTOs.Responses;
using MediatR;

namespace CommerceCore.Application.Commands.Create;

public record CreateMyCartCommand(Guid UserId)
    : IRequest<CartResponse>;

public class CreateMyCartCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateMyCartCommand, CartResponse>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<CartResponse> Handle(
        CreateMyCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var cart = new Cart
        {
            UserId = request.UserId
        };

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync(cancellationToken);

        return cart.ToDto();
    }
}
