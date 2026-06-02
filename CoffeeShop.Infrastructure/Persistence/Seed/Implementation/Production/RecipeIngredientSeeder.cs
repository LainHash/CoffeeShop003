using CoffeeShop.Domain.Entities.Production.Recipes;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Production;

public class RecipeIngredientSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.RecipeIngredients.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "RecipeIngredientData.csv");
        if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "RecipeIngredientData.csv");
        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<RecipeIngredientRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                context.RecipeIngredients.Add(new RecipeIngredient
                {
                    RecipeIngredientId = record.RecipeIngredientId,
                    RecipeId = record.RecipeId,
                    IngredientId = record.IngredientId,
                    Quantity = record.Quantity
                });
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RecipeIngredients ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RecipeIngredients OFF");
            await transaction.CommitAsync();
        });
    }

    private class RecipeIngredientRecord
    {
        public int RecipeIngredientId { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
    }
}
