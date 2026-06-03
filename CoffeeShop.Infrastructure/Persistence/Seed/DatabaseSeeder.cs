using CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog;
using CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Inventory;
using CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Misc;
using CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.Infrastructure.Persistence.Seed;

public class DatabaseSeeder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(IServiceProvider serviceProvider, ILogger<DatabaseSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CoffeeShopDbContext>();

        try
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }

            // Seed in order to respect foreign key constraints
            await SeedAsync<BrandSeeder>(context);
            await SeedAsync<CategorySeeder>(context);
            
            await SeedAsync<ProductSeeder>(context);
            await SeedAsync<IngredientSeeder>(context);
            
            await SeedAsync<ProductSkuSeeder>(context);
            await SeedAsync<IngredientSkuSeeder>(context);
            
            await SeedAsync<RecipeSeeder>(context);
            await SeedAsync<RecipeIngredientSeeder>(context);
            await SeedAsync<RecipeStepSeeder>(context);
            
            await SeedAsync<ImageSeeder>(context);
            await SeedAsync<StockTransactionSeeder>(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedAsync<TSeeder>(CoffeeShopDbContext context) where TSeeder : IDataSeeder
    {
        using var scope = _serviceProvider.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
        await seeder.SeedAsync(context);
    }
}
