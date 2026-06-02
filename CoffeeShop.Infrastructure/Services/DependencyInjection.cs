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

            return services;
        }
    }
}
