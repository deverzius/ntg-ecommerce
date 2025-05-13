using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Brand> Brands { get; set; }
    DbSet<ProductVariant> ProductVariants { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductImage> ProductImages { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    DbSet<Review> Reviews { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    void Dispose();
}
