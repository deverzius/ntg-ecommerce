using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceCore.Domain.Entities;

// TODO: should separate into multiple files
public class Brand
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }


    public virtual ICollection<Product> Products { get; set; }
}

public class ProductVariant
{
    [Key]
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public Guid ColorId { get; set; }

    public Guid SizeId { get; set; }


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
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(10)]
    public string HexCode { get; set; }
}

public class ProductSize
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}

public class Category
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public Guid? ParentCategoryId { get; set; }


    [ForeignKey("ParentCategoryId")]
    public virtual Category ParentCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}

public class ProductImage
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Url { get; set; }

    public Guid ProductId { get; set; }


    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}

public class ShoppingCart
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<ShoppingCartItem> Items { get; set; }
}

public class ShoppingCartItem
{
    [Key]
    public Guid Id { get; set; }

    public Guid ProductVariantId { get; set; }

    public int Quantity { get; set; }

    public Guid ShoppingCartId { get; set; }


    [ForeignKey("ShoppingCartId")]
    public ShoppingCart ShoppingCart { get; set; }

    [ForeignKey("ProductVariantId")]
    public virtual ProductVariant ProductVariant { get; set; }
}

public class Order
{
    [Key]
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    [Key]
    public Guid Id { get; set; }

    public Guid ProductVariantId { get; set; }

    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    public Guid OrderId { get; set; }

    [ForeignKey("ProductVariantId")]
    public virtual ProductVariant ProductVariant { get; set; }


    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
}

public class Review
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [Required]
    [StringLength(500)]
    public string Comment { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public Guid ProductId { get; set; }

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

    public virtual ICollection<ReviewImage> Images { get; set; }
}

public class ReviewImage
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Url { get; set; }

    public Guid ReviewId { get; set; }


    [ForeignKey("ReviewId")]
    public virtual Review Review { get; set; }
}
