using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using CoffeeShop.Application.Interfaces.Repositories.Production;
using CoffeeShop.Infrastructure.Persistence;
using CoffeeShop.Infrastructure.Persistence.Repositories.Catalog;
using CoffeeShop.Infrastructure.Persistence.Repositories.Production;
using CoffeeShop.Infrastructure.Persistence.Seed;
using CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Catalog;
using CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Inventory;
using CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Misc;
using CoffeeShop.Infrastructure.Persistence.Seed.Implementation.Production;
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
            services.AddScoped<DatabaseSeeder>();
            services.AddScoped<BrandSeeder>();
            services.AddScoped<CategorySeeder>();
            services.AddScoped<ProductSeeder>();
            services.AddScoped<IngredientSeeder>();
            services.AddScoped<ProductSkuSeeder>();
            services.AddScoped<IngredientSkuSeeder>();
            services.AddScoped<StockTransactionSeeder>();
            services.AddScoped<ImageSeeder>();
            services.AddScoped<RecipeSeeder>();
            services.AddScoped<RecipeIngredientSeeder>();
            services.AddScoped<RecipeStepSeeder>();

            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();

            return services;
        }
    }
}
