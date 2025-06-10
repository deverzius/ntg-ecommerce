namespace CommerceCore.Domain.Entities;

public class CartItem
{
    public required int Quantity { get; init; }

    public virtual ProductVariant ProductVariant { get; init; } = null!;
}
