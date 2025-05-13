using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class ProductVariant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public required Guid ProductId { get; set; }

    [MinLength(1)][StringLength(100)] public required string Name { get; set; }

    [MinLength(1)][StringLength(100)] public required string Value { get; set; }

    public required int StockQuantity { get; set; }

    [ForeignKey("ProductId")] public virtual Product? Product { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
}