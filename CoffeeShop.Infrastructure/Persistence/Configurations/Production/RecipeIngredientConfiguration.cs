using CoffeeShop.Domain.Entities.Production.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Production
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            // Typo fixed from 'RecipeIngrdients' to 'RecipeIngredients'
            builder.ToTable("RecipeIngredients");

            builder.HasKey(ri => ri.RecipeIngredientId);

            builder.Property(ri => ri.RecipeIngredientId)
                .UseIdentityColumn();

            builder.Property(ri => ri.RecipeId)
                .IsRequired();

            builder.Property(ri => ri.IngredientId)
                .IsRequired();

            builder.Property(ri => ri.Quantity)
                .IsRequired();

            // Relationships
            builder.HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
