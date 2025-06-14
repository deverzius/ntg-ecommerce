namespace CommerceCore.Domain.Entities;

public class VariantStock
{
    public Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required int Quantity { get; init; }
    public required ICollection<ProductVariant> Variants { get; init; } = [];
}
