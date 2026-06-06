using CoffeeShop.Domain.Entities.Catalog;
using CoffeeShop.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Inventory
{
    public class IngredientSkuConfiguration : IEntityTypeConfiguration<IngredientSku>
    {
        public void Configure(EntityTypeBuilder<IngredientSku> builder)
        {
            builder.ToTable("IngredientSkus");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .UseIdentityColumn();

            builder.Property(t => t.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(t => t.PublicId)
                .IsUnique();

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.Property(i => i.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(9, 2)");

            builder.Property(i => i.Unit)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(i => i.Stock)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(i => i.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(i => i.IngredientId)
                .IsRequired();

            // Unique Index
            builder.HasIndex(i => i.IngredientId)
                .IsUnique();

            // Check Constraint
            builder.ToTable(t => t.HasCheckConstraint("CK_IngredientSkus", "[Stock] >= 0"));

            // Relationship
            builder.HasOne(i => i.Ingredient)
                .WithOne(ig => ig.IngredientSku)
                .HasForeignKey<IngredientSku>(ig => ig.IngredientId)
                .HasPrincipalKey<Ingredient>(i => i.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
