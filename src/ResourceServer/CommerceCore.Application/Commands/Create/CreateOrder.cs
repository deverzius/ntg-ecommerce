using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Domain.Entities;
using CommerceCore.Domain.Enums;
using CommerceCore.Shared.Exceptions;
using MediatR;

namespace CommerceCore.Application.Commands.Create;

public record CreateOrderCommand(
    Guid UserId,
    CreateOrderDTO Order
) : IRequest<OrderResponse>;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, ICartRepository cartRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOrderCommand, OrderResponse>
{
    public async Task<OrderResponse> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken
    )
    {
        var cart = await cartRepository.GetCartByUserIdAsync(command.UserId, cancellationToken);
        if (cart is null)
        {
            throw new AppException(404, "Cart not found for the user.");
        }

        var cartItems = new List<CartItem>();
        foreach (var variantId in command.Order.ProductVariantIds)
        {
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductVariant.Id == variantId);
            if (cartItem is null)
            {
                throw new AppException(404, "Product variant not found in cart.");
            }

            cartItems.Add(cartItem);
            cart.CartItems.Remove(cartItem);
        }

        var orderItems = cartItems.Select(ci => new OrderItem
        {
            Quantity = ci.Quantity,
            ProductPrice = ci.ProductVariant.Product.Price,
            ProductName = ci.ProductVariant.Product.Name,
            ProductVariantName = ci.ProductVariant.Name,
            ProductVariantValue = ci.ProductVariant.Value,
            CurrentProductVariant = ci.ProductVariant
        }).ToList();

        var order = new Order
        {
            UserId = command.UserId,
            TotalPrice = cartItems.Sum(v => v.ProductVariant.Product.Price),
            ShippingAddress = command.Order.ShippingAddress,
            CustomerEmail = command.Order.CustomerEmail,
            CustomerName = command.Order.CustomerName,
            CreatedDate = DateTime.UtcNow,
            Status = EOrderStatus.Pending,
            OrderItems = orderItems
        };

        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);

        return order.ToDto();
    }
}