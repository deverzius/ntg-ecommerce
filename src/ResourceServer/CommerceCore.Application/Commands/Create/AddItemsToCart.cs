using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Shared.Exceptions;
using MediatR;

namespace CommerceCore.Application.Commands.Create;

public record AddItemsToCartCommand(Guid UserId, List<CreateCartItemDTO> Items)
    : IRequest;

public class AddItemsToCartCommandHandler(
    ICartRepository cartRepository,
    IProductVariantRepository variantRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<AddItemsToCartCommand>
{
    public async Task Handle(
        AddItemsToCartCommand command,
        CancellationToken cancellationToken
    )
    {
        var cart = await cartRepository.GetCartByUserId(command.UserId, cancellationToken);
        if (cart is null)
        {
            throw new AppException(404, "Cart not found for user.");
        }

        foreach (var item in command.Items)
        {
            var productVariant = await variantRepository.GetByIdAsync(item.ProductVariantId, cancellationToken);
            if (productVariant is null)
            {
                throw new AppException(404, "Product variant not found.");
            }

            var cartItem = new CartItem
            {
                Quantity = item.Quantity,
                ProductVariant = productVariant
            };

            cart.CartItems.Add(cartItem);
        }

        await unitOfWork.SaveAsync(cancellationToken);
    }
}
