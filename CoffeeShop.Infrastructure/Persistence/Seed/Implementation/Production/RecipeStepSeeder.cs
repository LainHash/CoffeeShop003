using CoffeeShop.Domain.Entities.Production.Recipes;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Production;

public class RecipeStepSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.RecipeSteps.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "RecipeStepData.csv");
        if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "RecipeStepData.csv");
        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<RecipeStepRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                context.RecipeSteps.Add(new RecipeStep
                {
                    Id = record.Id,
                    RecipeId = record.RecipeId,
                    StepNumber = record.StepNumber,
                    Description = record.Description,
                    DurationSeconds = record.DurationSeconds
                });
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RecipeSteps ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RecipeSteps OFF");
            await transaction.CommitAsync();
        });
    }

    private class RecipeStepRecord
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DurationSeconds { get; set; }
    }
}
