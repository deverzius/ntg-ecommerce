using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required int StockQuantity { get; init; }

    public virtual Product Product { get; init; } = null!;
    public virtual ICollection<AppImage> Images { get; init; } = [];
}
