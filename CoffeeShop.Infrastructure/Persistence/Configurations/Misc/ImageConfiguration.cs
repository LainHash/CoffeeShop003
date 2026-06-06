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

            builder.HasKey(i => i.Id);

            builder.HasIndex(x => new { x.ReferenceId, x.Type })
                .HasFilter("[IsPrimary] = 1")
                .IsUnique();

            builder.Property(i => i.Id)
                .UseIdentityColumn();

            builder.Property(r => r.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(r => r.PublicId)
                .IsUnique();

            builder.Property(r => r.Description)
                .HasMaxLength(500);

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
