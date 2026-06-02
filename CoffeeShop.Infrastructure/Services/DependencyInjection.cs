using CoffeeShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.Infrastructure.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection
        AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoffeeShopDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("MyConnectString")));

            // Seeders
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.DatabaseSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog.BrandSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog.CategorySeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog.ProductSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog.IngredientSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Inventory.ProductSkuSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Inventory.IngredientSkuSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Inventory.StockTransactionSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Misc.ImageSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Production.RecipeSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Production.RecipeIngredientSeeder>();
            services.AddScoped<CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Production.RecipeStepSeeder>();

            return services;
        }
    }
}
