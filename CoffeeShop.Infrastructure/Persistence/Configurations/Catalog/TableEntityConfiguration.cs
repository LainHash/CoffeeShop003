using CoffeeShop.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Catalog
{
    public class TableEntityConfiguration : IEntityTypeConfiguration<TableEntity>
    {
        public void Configure(EntityTypeBuilder<TableEntity> builder)
        {
            builder.ToTable("TableEntities");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .UseIdentityColumn();

            builder.Property(t => t.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(t => t.PublicId)
                .IsUnique();

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.Property(t => t.Shape)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.TableNumber)
                .IsRequired();

            builder.Property(t => t.FloorNumber)
                .IsRequired();

            builder.HasIndex(t => new { t.TableNumber, t.FloorNumber })
                .IsUnique();

            builder.Property(t => t.Capacity)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired()
                .HasDefaultValue("Available")
                .HasMaxLength(20);

            builder.Property(t => t.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(t => t.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("sysdatetime()");

            builder.Property(t => t.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(i => i.DeletedAt)
                .IsRequired(false)
                .HasDefaultValue(null);
        }
    }
}
