using CoffeeShop.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductId)
                .UseIdentityColumn();

            builder.Property(p => p.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(p => p.PublicId)
                .IsUnique();

            builder.Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.IsMadeToOrder)
                .IsRequired();

            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.Property(p => p.BrandId)
                .IsRequired(false);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
