namespace CoffeeShop.Infrastructure.Persistence.Seed;

public interface IDataSeeder
{
    Task SeedAsync(CoffeeShopDbContext context);
}
