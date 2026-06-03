using CoffeeShop.Domain.Entities.Catalog;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog;

public class IngredientSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.Ingredients.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "IngredientData.csv");
        if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "IngredientData.csv");
        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<IngredientRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                context.Ingredients.Add(new Ingredient
                {
                    IngredientId = record.IngredientId,
                    IngredientName = record.IngredientName,
                    Description = record.Description,
                    BrandId = record.BrandId,
                    CategoryId = record.CategoryId
                });
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Ingredients ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Ingredients OFF");
            await transaction.CommitAsync();
        });
    }

    private class IngredientRecord
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
