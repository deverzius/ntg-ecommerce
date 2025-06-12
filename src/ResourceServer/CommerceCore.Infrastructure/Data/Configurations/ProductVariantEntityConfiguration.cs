using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.Property(pv => pv.DisplayValue)
            .IsRequired();

        // Relationships
        builder.HasMany(pv => pv.Images)
            .WithMany();
    }
}
