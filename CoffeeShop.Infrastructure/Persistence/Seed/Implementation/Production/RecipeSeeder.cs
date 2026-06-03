using CoffeeShop.Domain.Entities.Production.Recipes;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Production;

public class RecipeSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.Recipes.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "RecipeData.csv");
        if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "RecipeData.csv");
        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<RecipeRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                context.Recipes.Add(new Recipe
                {
                    RecipeId = record.RecipeId,
                    Inspiration = record.Inspiration ?? string.Empty,
                    ProductId = record.ProductId
                });
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Recipes ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Recipes OFF");
            await transaction.CommitAsync();
        });
    }

    private class RecipeRecord
    {
        public int RecipeId { get; set; }
        public string? Inspiration { get; set; }
        public int ProductId { get; set; }
    }
}
