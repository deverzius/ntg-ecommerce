namespace CommerceCore.Application.Common.DTOs;

public record CreateOrderDTO(
    string ShippingAddress,
    string CustomerEmail,
    string CustomerName,
    List<Guid> ProductVariantIds
);