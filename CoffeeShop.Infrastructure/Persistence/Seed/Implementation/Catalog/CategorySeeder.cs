using CoffeeShop.Domain.Entities.Catalog;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog;

public class CategorySeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "CategoryData.csv");
        if (!File.Exists(path))
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "CategoryData.csv");
        }

        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<CategoryRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                context.Categories.Add(new Category
                {
                    CategoryId = record.CategoryId,
                    CategoryName = record.CategoryName,
                    Description = record.Description
                });
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Categories OFF");
            await transaction.CommitAsync();
        });
    }

    private class CategoryRecord
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
