namespace CommerceCore.Domain.Entities;

public class Product
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }

    public virtual Category Category { get; init; } = null!;
    public virtual ICollection<ProductVariant> Variants { get; init; } = [];
}
