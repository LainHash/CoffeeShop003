using CoffeeShop.Domain.Entities.Inventory;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Inventory;

public class ProductSkuSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.ProductSkus.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "ProductSkuData.csv");
        if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "ProductSkuData.csv");
        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<ProductSkuRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                context.ProductSkus.Add(new ProductSku
                {
                    ProductSkuId = record.ProductSkuId,
                    ProductId = record.ProductId,
                    UnitPrice = record.UnitPrice,
                    Stock = record.Stock,
                    Unit = record.Unit,
                    Status = record.Status
                });
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductSkus ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductSkus OFF");
            await transaction.CommitAsync();
        });
    }

    private class ProductSkuRecord
    {
        public int ProductSkuId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
