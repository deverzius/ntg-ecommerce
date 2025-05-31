using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CommerceCore.Infrastructure.Data.Configurations;

public class ProductVariantEntityConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasKey(pv => pv.Id);
        builder.Property(pv => pv.Id)
            .ValueGeneratedOnAdd();

        builder.Property(pv => pv.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pv => pv.Value)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pv => pv.StockQuantity)
            .IsRequired();

        // Relationships
        builder.HasMany(c => c.Images)
            .WithMany();
    }
}
