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

        public async Task<Result<List<BrandDTO>>> GetBrandsAsync(CancellationToken cancellationToken = default)
        {
            var brands = await _context.Brands.Select(b => new BrandDTO
            {
                Name = b.Name,
                Description = b.Description
            }).ToListAsync(cancellationToken);
            return Result<List<BrandDTO>>
                .SuccessResponse(brands, "Brands retrieved successfully.");
        }
    }
}