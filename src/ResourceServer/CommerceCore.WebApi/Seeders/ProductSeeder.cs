using CommerceCore.Domain.Entities;
using CommerceCore.Infrastructure.Data;

namespace CommerceCore.WebAPI.Seeders;

public static class ProductSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Categories.Any())
        {
            var categories = new Category[]
            {
                new() { Id = Guid.NewGuid(), Name = "Tops", Description = "Description for Tops" },
                new() { Id = Guid.NewGuid(), Name = "Bottoms", Description = "Description for Bottoms" },
                new() { Id = Guid.NewGuid(), Name = "Bags", Description = "Description for Bags" },
                new() { Id = Guid.NewGuid(), Name = "Accessories", Description = "Description for Accessories" }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            var products = new Product[]
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Casual T-Shirt",
                    Description = "A comfortable and stylish casual t-shirt.",
                    Price = 19.99m,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Category = categories[0],
                    Variants = new List<ProductVariant>
                    {
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorRed", DisplayValue = "Red" },
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorBlue", DisplayValue = "Blue" },
                        new() { Id = Guid.NewGuid(), Name = "Size", Value = "SizeM", DisplayValue = "M" },
                        new() { Id = Guid.NewGuid(), Name = "Size", Value = "SizeL", DisplayValue = "L" }
                    }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Formal Shirt",
                    Description = "A formal shirt for business occasions.",
                    Price = 39.99m,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Category = categories[0],
                    Variants = new List<ProductVariant>
                    {
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorWhite", DisplayValue = "White" },
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorBlack", DisplayValue = "Black" },
                        new() { Id = Guid.NewGuid(), Name = "Size", Value = "SizeS", DisplayValue = "S" },
                        new() { Id = Guid.NewGuid(), Name = "Size", Value = "SizeXL", DisplayValue = "XL" }
                    }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Denim Jeans",
                    Description = "Classic denim jeans for everyday wear.",
                    Price = 49.99m,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Category = categories[1],
                    Variants = new List<ProductVariant>
                    {
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorBlue", DisplayValue = "Blue" },
                        new() { Id = Guid.NewGuid(), Name = "Size", Value = "Size32", DisplayValue = "32" },
                        new() { Id = Guid.NewGuid(), Name = "Size", Value = "Size34", DisplayValue = "34" }
                    }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Leather Handbag",
                    Description = "A stylish leather handbag for every occasion.",
                    Price = 89.99m,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Category = categories[2],
                    Variants = new List<ProductVariant>
                    {
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorBlack", DisplayValue = "Black" },
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorBrown", DisplayValue = "Brown" }
                    }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Wool Scarf",
                    Description = "A warm wool scarf for the winter season.",
                    Price = 29.99m,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Category = categories[3],
                    Variants = new List<ProductVariant>
                    {
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorGray", DisplayValue = "Gray" },
                        new() { Id = Guid.NewGuid(), Name = "Color", Value = "ColorNavy", DisplayValue = "Navy" }
                    }
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}