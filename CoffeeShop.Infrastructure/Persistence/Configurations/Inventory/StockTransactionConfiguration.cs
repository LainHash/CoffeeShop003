using CoffeeShop.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Inventory
{
    public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.ToTable("StockTransactions");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .UseIdentityColumn();

            builder.Property(t => t.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(t => t.PublicId)
                .IsUnique();

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.Property(s => s.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(s => s.Quantity)
                .IsRequired();

            builder.Property(s => s.Note)
                .HasMaxLength(500);

            builder.Property(s => s.ReferenceId)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .HasDefaultValueSql("sysdatetime()");
        }
    }
}
