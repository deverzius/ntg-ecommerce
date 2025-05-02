using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class ProductImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Url { get; set; }

    [Required]
    public required Guid ProductId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }
}
