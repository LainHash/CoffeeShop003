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

            builder.HasKey(s => s.StockTransactionId);

            builder.Property(s => s.StockTransactionId)
                .UseIdentityColumn();

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
