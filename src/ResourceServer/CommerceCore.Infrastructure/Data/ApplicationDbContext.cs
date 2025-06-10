using System.Reflection;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<AppImage> Images { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
