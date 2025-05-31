using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required int StockQuantity { get; init; }
    public required Guid ProductId { get; init; }
    public virtual ICollection<Image> Images { get; init; } = [];
}
