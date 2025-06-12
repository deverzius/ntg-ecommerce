using CommerceCore.Domain.Entities;
using CommerceCore.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Commands.Create;

public record AddItemsToCartCommand(Guid UserId, List<CreateCartItemDTO> Items)
    : IRequest;

public class AddItemsToCartCommandHandler(IApplicationDbContext context)
    : IRequestHandler<AddItemsToCartCommand>
{
    public async Task Handle(
        AddItemsToCartCommand command,
        CancellationToken cancellationToken
    )
    {
        var cart = context.Carts.FirstOrDefault(c => c.UserId == command.UserId);
        if (cart is null)
        {
            throw new AppException(404, "Cart not found for user.");
        }

        foreach (var item in command.Items)
        {
            var productVariant = await context.ProductVariants
                .Include(pv => pv.Product)
                .FirstOrDefaultAsync(pv => pv.Id == item.ProductVariantId, cancellationToken);

            if (productVariant is null)
            {
                throw new AppException(404, $"Product variant with ID {item.ProductVariantId} not found.");
            }

            var cartItem = new CartItem
            {
                Quantity = item.Quantity,
                ProductVariant = productVariant
            };

            cart.CartItems.Add(cartItem);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
