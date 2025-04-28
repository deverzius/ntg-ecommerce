using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

// TODO: should separate into multiple files
public class ProductVariant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public required Guid ProductId { get; set; }

    public required Guid ColorId { get; set; }

    public required Guid SizeId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("SizeId")]
    public virtual ProductSize Size { get; set; }

    [ForeignKey("ColorId")]
    public virtual ProductColor Color { get; set; }
}

public class ProductColor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    [StringLength(10)]
    public required string HexCode { get; set; }

    [Required]
    [StringLength(100)]
    public required string Description { get; set; }
}

public class ProductSize
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    [StringLength(100)]
    public required string Description { get; set; }
}

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
    public virtual Product Product { get; set; }
}

public class ShoppingCart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public required Guid UserId { get; set; }

    public virtual ICollection<ShoppingCartItem> Items { get; set; } = [];
}

public class ShoppingCartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public required Guid ProductVariantId { get; set; }

    [Required]
    public required int Quantity { get; set; }

    [Required]
    public required Guid ShoppingCartId { get; set; }

    [ForeignKey("ShoppingCartId")]
    public virtual ShoppingCart ShoppingCart { get; set; }

    [ForeignKey("ProductVariantId")]
    public virtual ProductVariant ProductVariant { get; set; }
}

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
    public required DateTime OrderDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
}

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public required Guid ProductVariantId { get; set; }

    [Required]
    public required int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public required decimal Price { get; set; }

    [Required]
    public required Guid OrderId { get; set; }

    [ForeignKey("ProductVariantId")]
    public virtual ProductVariant ProductVariant { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
}

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [Range(1, 5)]
    public required int Rating { get; set; }

    [Required]
    [StringLength(200)]
    public required string Title { get; set; }

    [Required]
    [StringLength(500)]
    public required string Comment { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Required]
    public required Guid ProductId { get; set; }

    public Guid? UserId { get; set; }

    // Required when UserId is null
    public string? FullName { get; set; }

    // Required when UserId is null
    [Phone]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    public virtual ICollection<ReviewImage> Images { get; set; } = [];
}

public class ReviewImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Url { get; set; }

    [Required]
    public required Guid ReviewId { get; set; }

    [ForeignKey("ReviewId")]
    public virtual Review Review { get; set; }
}
