using CoffeeShop.Domain.Entities.Inventory;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Inventory;

public class StockTransactionSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.StockTransactions.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "StockTransactionData.csv");
        if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "StockTransactionData.csv");
        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<StockTransactionRecord>().ToList();
        if (!records.Any()) return;

        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                context.StockTransactions.Add(new StockTransaction
                {
                    StockTransactionId = record.StockTransactionId,
                    Type = record.Type,
                    Quantity = record.Quantity,
                    Note = record.Note,
                    ReferenceId = record.ReferenceId
                });
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT StockTransactions ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT StockTransactions OFF");
            await transaction.CommitAsync();
        });
    }

    private class StockTransactionRecord
    {
        public int StockTransactionId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public int ReferenceId { get; set; }
    }
}
