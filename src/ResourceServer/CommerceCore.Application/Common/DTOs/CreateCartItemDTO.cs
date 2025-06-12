namespace CommerceCore.Application.Common.DTOs;

public record CreateCartItemDTO(Guid ProductVariantId, int Quantity);