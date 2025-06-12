namespace CommerceCore.Shared.DTOs.Responses;

public record CartResponse(
   Guid UserId,
   ICollection<CartItemResponse> CartItems
);
