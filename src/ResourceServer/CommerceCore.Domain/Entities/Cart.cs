namespace CommerceCore.Domain.Entities;

public class Cart
{
    public Guid UserId { get; init; }

    public ICollection<CartItem> CartItems { get; init; } = [];
}
