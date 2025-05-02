using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public required int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public required decimal Price { get; set; }

    [Required]
    public required Guid OrderId { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = [];

    [ForeignKey("OrderId")]
    public virtual Order? Order { get; set; }
}
