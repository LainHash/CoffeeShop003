using CoffeeShop.Domain.Entities.Catalog;
using CoffeeShop.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Inventory
{
    public class ProductSkuConfiguration : IEntityTypeConfiguration<ProductSku>
    {
        public void Configure(EntityTypeBuilder<ProductSku> builder)
        {
            builder.ToTable("ProductSkus");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseIdentityColumn();

            builder.Property(t => t.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(t => t.PublicId)
                .IsUnique();

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.Property(p => p.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(9, 2)");

            builder.Property(p => p.Unit)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Stock)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.ProductId)
                .IsRequired();

            // Unique Index
            builder.HasIndex(p => p.ProductId)
                .IsUnique();

            // Check Constraint
            builder.ToTable(t => t.HasCheckConstraint("CK_ProductSkus", "[Stock] >= 0"));

            // Relationship
            builder.HasOne(p => p.Product)
                .WithOne(ps => ps.ProductSku)
                .HasForeignKey<ProductSku>(ps => ps.ProductId)
                .HasPrincipalKey<Product>(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
