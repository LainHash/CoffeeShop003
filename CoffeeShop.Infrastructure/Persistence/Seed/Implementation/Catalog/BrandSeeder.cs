using CoffeeShop.Domain.Entities.Catalog;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog;

public class BrandSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.Brands.AnyAsync())
            return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "BrandData.csv");
        if (!File.Exists(path))
        {
            // Fallback for development
            path = Path.Combine(Directory.GetCurrentDirectory(), "Persistence", "Seed", "Data", "BrandData.csv");
            if (!File.Exists(path))
                path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "BrandData.csv");
        }

        if (!File.Exists(path))
            return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null
        });

        var records = csv.GetRecords<BrandRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            
            foreach (var record in records)
            {
                var brand = new Brand
                {
                    BrandId = record.BrandId,
                    BrandName = record.BrandName,
                    Description = record.Description
                };
                context.Brands.Add(brand);
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Brands ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Brands OFF");
            
            await transaction.CommitAsync();
        });
    }

    private class BrandRecord
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
