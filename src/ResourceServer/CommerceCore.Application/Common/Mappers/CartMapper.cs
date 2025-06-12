using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappers;

public static class CartMapper
{
    public static CartResponse ToDto(this Cart cart)
    {
        return new CartResponse(
            cart.UserId,
            cart.CartItems.Select(ci => ci.ToDto()).ToList()
        );
    }

    public static CartItemResponse ToDto(this CartItem cartItem)
    {
        return new CartItemResponse(
            cartItem.Quantity,
            cartItem.ProductVariant.ToDto()
        );
    }
}
