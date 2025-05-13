using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    [MinLength(1)]
    public required string Name { get; set; }

    [Required][StringLength(500)] public required string Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(double.MinValue, double.MaxValue)]
    public required decimal Price { get; set; }

    [Required] public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Required] public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    [Required] public required Guid BrandId { get; set; }

    [Required] public required Guid CategoryId { get; set; }

    [ForeignKey("BrandId")] public virtual Brand? Brand { get; set; }

    [ForeignKey("CategoryId")] public virtual Category? Category { get; set; }

    public virtual ICollection<ProductImage> Images { get; set; } = [];
    public virtual ICollection<ProductVariant> Variants { get; set; } = [];
    public virtual ICollection<Review> Reviews { get; set; } = [];
}