using System.ComponentModel.DataAnnotations;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CommerceCore.Infrastructure.Data.Configurations;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Id)
            .ValueGeneratedOnAdd();

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.Property(oi => oi.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(oi => oi.ProductVariantId)
            .IsRequired();

        builder.Property(oi => oi.OrderId)
            .IsRequired();

        // Relationships
        builder.HasOne(oi => oi.ProductVariant)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oi => oi.ProductVariant)
            .WithOne()
            .HasForeignKey<OrderItem>(oi => oi.ProductVariantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
