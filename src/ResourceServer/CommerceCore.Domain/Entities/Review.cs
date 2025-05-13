using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required][Range(1, 5)] public required int Rating { get; set; }

    [Required][StringLength(200)] public required string Title { get; set; }

    [Required][StringLength(500)] public required string Comment { get; set; }

    [Required] public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Required] public required Guid ProductId { get; set; }

    // Required when UserId is null
    public string? FullName { get; set; }

    // Required when UserId is null
    [Phone] public string? PhoneNumber { get; set; }

    [EmailAddress] public string? Email { get; set; }

    [ForeignKey("ProductId")] public virtual Product? Product { get; set; }
}