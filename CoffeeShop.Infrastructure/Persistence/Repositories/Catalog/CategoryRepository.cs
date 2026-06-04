using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Categories.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Persistence.Repositories.Catalog
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CoffeeShopDbContext _context;
        public CategoryRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CategoryDTO>>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDTO
                {
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync(cancellationToken);
            return Result<List<CategoryDTO>>
                .SuccessResponse(categories, "Categories retrieved successfully.");
        }
    }
}
