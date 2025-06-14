using CommerceCore.Domain.Entities;

namespace CommerceCore.Application.Common.Mappers;

public static class OrderMapper
{
    public static OrderResponse ToDto(this Order order)
    {
        return new OrderResponse(
            order.Id,
            order.UserId,
            order.TotalPrice,
            order.ShippingAddress,
            order.CustomerEmail,
            order.CustomerName,
            order.CreatedDate,
            order.Status.ToString(),
            order.OrderItems.Select(oi => oi.ToDto()).ToList()
        );
    }

    public static OrderItemResponse ToDto(this OrderItem orderItem)
    {
        return new OrderItemResponse(
            orderItem.Quantity,
            orderItem.ProductPrice,
            orderItem.ProductName,
            orderItem.ProductVariantName,
            orderItem.ProductVariantValue,
            orderItem.CurrentProduct.ToDto(),
            orderItem.CurrentProductVariant.ToDto()
        );
    }
}