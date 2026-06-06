using CoffeeShop.Domain.Entities.Catalog;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog
{
    public class TableSeeder : IDataSeeder
    {
        public async Task SeedAsync(CoffeeShopDbContext context)
        {
            if (await context.TableEntities.AnyAsync()) return;

            var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "TableData.csv");
            if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "TableData.csv");
            if (!File.Exists(path)) return;

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

            var records = csv.GetRecords<TableRecord>().ToList();

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                foreach (var record in records)
                {
                    var tableEntity = new TableEntity
                    {
                        Id = record.Id,
                        Shape = record.Shape,
                        TableNumber = record.TableNumber,
                        FloorNumber = record.FloorNumber,
                        Capacity = record.Capacity,
                        Status = record.Status
                    };
                    context.TableEntities.Add(tableEntity);
                }
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TableEntities ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TableEntities OFF");
                await transaction.CommitAsync();
            });
        }
    }

    public class TableRecord
    {
        public int Id { get; set; }
        public string Shape { get; set; } = null!;
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; } = null!;
    }
}
