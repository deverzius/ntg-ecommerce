using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommerceCore.Infrastructure.Data.Configurations;

public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.UserId);

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.OwnsMany(c => c.CartItems, ci =>
        {
            ci.Property(c => c.Quantity)
                .IsRequired()
                .HasDefaultValue(1);

            ci.HasOne(c => c.ProductVariant)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}