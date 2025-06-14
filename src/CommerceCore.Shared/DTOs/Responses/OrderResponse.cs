namespace CommerceCore.Shared.DTOs.Responses;

public record OrderResponse(
    Guid Id,
    Guid UserId,
    decimal TotalPrice,
    string ShippingAddress,
    string CustomerEmail,
    string CustomerName,
    DateTime CreatedDate,
    string Status,
    ICollection<OrderItemResponse> OrderItems
);
