using CoffeeShop.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace CoffeeShop.Infrastructure.Persistence.Seed;

public interface IDataSeeder
{
    Task SeedAsync(CoffeeShopDbContext context);
}
