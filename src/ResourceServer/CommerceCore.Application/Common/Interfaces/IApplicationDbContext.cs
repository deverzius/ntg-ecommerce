using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<AppImage> Images { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductVariant> ProductVariants { get; set; }
    DbSet<Review> Reviews { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<CartItem> CartItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    void Dispose();
}
