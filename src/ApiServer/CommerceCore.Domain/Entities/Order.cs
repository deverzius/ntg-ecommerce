using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,4)")]
    public required decimal TotalPrice { get; set; }

    [Required]
    public required DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
}
