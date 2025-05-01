using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Brand> Brands { get; set; }
    DbSet<ProductVariant> ProductVariants { get; set; }
    DbSet<ProductColor> ProductColors { get; set; }
    DbSet<ProductSize> ProductSizes { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductImage> ProductImages { get; set; }
    DbSet<ShoppingCart> ShoppingCarts { get; set; }
    DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<ReviewImage> ReviewImages { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
