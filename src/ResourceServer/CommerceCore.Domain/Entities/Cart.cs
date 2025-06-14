namespace CommerceCore.Domain.Entities;

public class Cart
{
    public Guid UserId { get; set; }

    public ICollection<CartItem> CartItems { get; init; } = [];
}
