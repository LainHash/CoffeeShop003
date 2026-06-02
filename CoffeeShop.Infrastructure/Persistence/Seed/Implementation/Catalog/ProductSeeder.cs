using CoffeeShop.Domain.Entities.Catalog;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog;

public class ProductSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.Products.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "ProductData.csv");
        if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "ProductData.csv");
        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<ProductRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                context.Products.Add(new Product
                {
                    ProductId = record.ProductId,
                    ProductName = record.ProductName,
                    Description = record.Description,
                    CategoryId = record.CategoryId,
                    IsMadeToOrder = record.IsMadeToOrder == 1
                });
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Products ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Products OFF");
            await transaction.CommitAsync();
        });
    }

    private class ProductRecord
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int IsMadeToOrder { get; set; }
    }
}
