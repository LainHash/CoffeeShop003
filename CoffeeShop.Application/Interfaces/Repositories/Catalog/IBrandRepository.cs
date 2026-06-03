using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Brands.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Catalog
{
    public interface IBrandRepository
    {
        Task<Result<List<BrandDTO>>> GetBrandsAsync();
    }
}
