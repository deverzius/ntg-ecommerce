using CommerceCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CommerceCore.Infrastructure.Data.Configurations;

public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Url)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Type)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(i => i.UploadedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
