using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
    public required Guid ProductVariantId { get; init; }
    public required Guid OrderId { get; init; }

    public virtual ProductVariant ProductVariant { get; init; } = null!;
}
