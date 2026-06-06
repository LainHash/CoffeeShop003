using CoffeeShop.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Catalog
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("Ingredients");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .UseIdentityColumn();

            builder.Property(i => i.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(i => i.PublicId)
                .IsUnique();

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.Property(i => i.BrandId)
                .IsRequired();

            builder.Property(i => i.CategoryId)
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(i => i.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(i => i.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(i => i.DeletedAt)
                .IsRequired(false)
                .HasDefaultValue(null);

            // Relationships
            builder.HasOne(i => i.Brand)
                .WithMany(b => b.Ingredients)
                .HasForeignKey(i => i.BrandId)
                .HasPrincipalKey(b => b.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Category)
                .WithMany(c => c.Ingredients)
                .HasForeignKey(i => i.CategoryId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
