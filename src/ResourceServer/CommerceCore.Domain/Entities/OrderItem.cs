namespace CommerceCore.Domain.Entities;

public class OrderItem
{
    public required int Quantity { get; init; }
    public required decimal ProductPrice { get; init; }
    public required string ProductName { get; init; }
    public required string ProductVariantName { get; init; }
    public required string ProductVariantValue { get; init; }

    public required ProductVariant CurrentProductVariant { get; init; } = null!;
}
