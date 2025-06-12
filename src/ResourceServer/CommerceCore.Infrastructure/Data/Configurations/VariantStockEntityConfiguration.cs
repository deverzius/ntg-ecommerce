using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommerceCore.Infrastructure.Data.Configurations;

public class VariantStockEntityConfiguration : IEntityTypeConfiguration<VariantStock>
{
    public void Configure(EntityTypeBuilder<VariantStock> builder)
    {
        builder.HasKey(vs => vs.Id);
        builder.Property(vs => vs.Id)
            .ValueGeneratedOnAdd();

        builder.Property(vs => vs.ProductId)
            .IsRequired();

        builder.Property(vs => vs.Quantity)
            .IsRequired();

        // Relationships
        builder.HasMany(c => c.Variants)
            .WithMany();
    }
}
