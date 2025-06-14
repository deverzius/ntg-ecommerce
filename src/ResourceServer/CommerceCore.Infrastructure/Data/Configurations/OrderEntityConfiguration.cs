using CommerceCore.Domain.Entities;
using CommerceCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommerceCore.Infrastructure.Data.Configurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(o => o.ShippingAddress)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.CustomerEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(o => o.Status)
            .IsRequired()
            .HasDefaultValue(EOrderStatus.Pending);

        builder.OwnsMany(o => o.OrderItems, oi =>
        {
            oi.Property(oi => oi.Quantity)
                .IsRequired();

            oi.Property(oi => oi.ProductPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            oi.Property(oi => oi.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            oi.Property(oi => oi.ProductVariantName)
                .IsRequired()
                .HasMaxLength(100);

            oi.Property(oi => oi.ProductVariantValue)
                .IsRequired()
                .HasMaxLength(100);

            oi.HasOne(oi => oi.CurrentProductVariant)
                .WithMany()
                .IsRequired();
        });
    }
}
