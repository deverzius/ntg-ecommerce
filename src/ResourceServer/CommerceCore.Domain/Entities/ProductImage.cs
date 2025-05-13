using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class ProductImage
{
    [Key][Required][MaxLength(100)] public required string Name { get; set; }

    [Required][MaxLength(200)] public required string Path { get; set; }

    [Required] public required Guid ProductId { get; set; }

    [ForeignKey("ProductId")] public virtual Product? Product { get; set; }
}