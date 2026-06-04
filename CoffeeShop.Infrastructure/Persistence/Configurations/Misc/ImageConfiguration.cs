using CoffeeShop.Domain.Entities.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Misc
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(i => i.ImageId);

            builder.HasIndex(i => new { i.ReferenceId, i.Type, i.IsPrimary });

            builder.Property(i => i.ImageId)
                .UseIdentityColumn();

            builder.Property(i => i.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(i => i.IsPrimary)
                .IsRequired();

            builder.Property(i => i.ReferenceId)
                .IsRequired();

            builder.Property(i => i.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(i => i.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");
        }
    }
}
