using System.ComponentModel.DataAnnotations;
using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CommerceCore.Infrastructure.Data.Configurations;

public class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Rating)
            .IsRequired()
            .HasAnnotation("Range", new RangeAttribute(1, 5));

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Comment)
            .IsRequired()
            .HasMaxLength(400);

        builder.Property(r => r.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(r => r.ProductId)
            .IsRequired();

        builder.Property(r => r.FullName)
            .IsRequired(false);

        builder.Property(r => r.PhoneNumber)
            .IsRequired(false)
            .HasAnnotation("Phone", new PhoneAttribute());

        builder.Property(r => r.Email)
            .IsRequired(false)
            .HasAnnotation("EmailAddress", new EmailAddressAttribute());
    }
}
