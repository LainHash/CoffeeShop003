using CoffeeShop.Domain.Entities.Catalog;
using CoffeeShop.Domain.Entities.Inventory;
using CoffeeShop.Domain.Entities.Misc;
using CoffeeShop.Domain.Entities.Production.Recipes;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Persistence
{
    public class CoffeeShopDbContext : DbContext
    {
        public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options)
        {
        }

        // Catalog
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<TableEntity> TableEntities => Set<TableEntity>();

        // Inventory
        public DbSet<ProductSku> ProductSkus => Set<ProductSku>();
        public DbSet<IngredientSku> IngredientSkus => Set<IngredientSku>();
        public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();

        // Production
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
        public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoffeeShopDbContext).Assembly);
        }
    }
}
