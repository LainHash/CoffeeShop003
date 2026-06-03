using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Brands.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Persistence.Repositories.Catalog
{
    public class BrandRepository : IBrandRepository
    {
        private readonly CoffeeShopDbContext _context;
        public BrandRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<BrandDTO>>> GetBrandsAsync()
        {
            var brands = await _context.Brands.Select(b => new BrandDTO
            {
                BrandName = b.BrandName,
                Description = b.Description
            }).ToListAsync();
            return Result<List<BrandDTO>>.SuccessResponse(brands, "Brands retrieved successfully.");
        }
    }
}