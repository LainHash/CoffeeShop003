using CoffeeShop.Domain.Entities.Misc;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using CoffeeShop.Domain.Enums;

namespace CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Misc;

public class ImageSeeder : IDataSeeder
{
    public async Task SeedAsync(CoffeeShopDbContext context)
    {
        if (await context.Images.AnyAsync()) return;

        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "Data", "ImageData.csv");
        if (!File.Exists(path)) path = Path.Combine(Directory.GetCurrentDirectory(), "CoffeeShop.Infrastructure", "Persistence", "Seed", "Data", "ImageData.csv");
        if (!File.Exists(path)) return;

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true });

        var records = csv.GetRecords<ImageRecord>().ToList();
        
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            foreach (var record in records)
            {
                if (Enum.TryParse<ReferenceType>(record.Type, out var type))
                {
                    context.Images.Add(new Image
                    {
                        ImageId = record.ImageId,
                        ImageUrl = record.ImageUrl,
                        IsPrimary = record.IsPrimary == 1,
                        ReferenceId = record.ReferenceId,
                        Type = type
                    });
                }
            }

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Images ON");
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Images OFF");
            await transaction.CommitAsync();
        });
    }

    private class ImageRecord
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int IsPrimary { get; set; }
        public int ReferenceId { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
