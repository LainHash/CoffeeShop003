using CoffeeShop.Domain.Entities.Production.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Production
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");

            builder.HasKey(r => r.RecipeId);

            builder.Property(r => r.RecipeId)
                .UseIdentityColumn();

            builder.Property(r => r.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(r => r.PublicId)
                .IsUnique();

            builder.Property(r => r.Inspiration)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .HasMaxLength(500);

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(r => r.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(r => r.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(r => r.ProductId)
                .IsRequired();

            // Relationship
            builder.HasOne(r => r.Product)
                .WithMany(p => p.Recipes)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
