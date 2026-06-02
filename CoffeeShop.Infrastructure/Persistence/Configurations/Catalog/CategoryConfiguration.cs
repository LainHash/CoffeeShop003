using CoffeeShop.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Catalog
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.CategoryId)
                .UseIdentityColumn();

            builder.Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(c => c.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
