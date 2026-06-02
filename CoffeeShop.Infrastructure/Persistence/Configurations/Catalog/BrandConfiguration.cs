using CoffeeShop.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Catalog
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.HasKey(b => b.BrandId);

            builder.Property(b => b.BrandId)
                .UseIdentityColumn();

            builder.Property(b => b.BrandName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Description)
                .HasMaxLength(500);

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(b => b.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(b => b.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
