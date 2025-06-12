using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using MediatR;

namespace CommerceCore.Application.Commands.Create;

public record CreateMyCartCommand(Guid UserId)
    : IRequest<CartResponse>;

public class CreateMyCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateMyCartCommand, CartResponse>
{
    public async Task<CartResponse> Handle(
        CreateMyCartCommand request,
        CancellationToken cancellationToken
    )
    {
        var cart = new Cart
        {
            UserId = request.UserId
        };

        await cartRepository.AddAsync(cart, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);

        return cart.ToDto();
    }
}
