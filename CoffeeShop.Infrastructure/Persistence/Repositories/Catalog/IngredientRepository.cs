using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Persistence.Repositories.Catalog
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly CoffeeShopDbContext _context;
        public IngredientRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<IngredientDTO>>> GetIngredientsAsync(CancellationToken cancellationToken = default)
        {
            var ingredients = await _context.Ingredients
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.IngredientSku)
                .Where(i => !i.IsDeleted)
                .Select(i => new IngredientDTO
                {
                    PublicId = i.PublicId,
                    IngredientName = i.IngredientName,
                    Description = i.Description,
                    BrandName = i.Brand.BrandName,
                    CategoryName = i.Category.CategoryName,
                    UnitPrice = i.IngredientSku.UnitPrice,
                    Unit = i.IngredientSku.Unit,
                    Stock = i.IngredientSku.Stock,
                    Status = i.IngredientSku.Status,
                    CreatedAt = i.CreatedAt,
                    UpdatedAt = i.UpdatedAt,
                    IsDeleted = i.IsDeleted
                })
                .ToListAsync(cancellationToken);
            return Result<List<IngredientDTO>>.SuccessResponse(ingredients, "Ingredients retrieved successfully.");
        }

        public async Task<Result<IngredientDTO>> GetIngredientByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            var ingredient = await _context.Ingredients
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.IngredientSku)
                .Where(i => !i.IsDeleted && i.PublicId == publicId)
                .Select(i => new IngredientDTO
                {
                    PublicId = i.PublicId,
                    IngredientName = i.IngredientName,
                    Description = i.Description,
                    BrandName = i.Brand.BrandName,
                    CategoryName = i.Category.CategoryName,
                    UnitPrice = i.IngredientSku!.UnitPrice,
                    Unit = i.IngredientSku!.Unit,
                    Stock = i.IngredientSku!.Stock,
                    Status = i.IngredientSku!.Status,
                    CreatedAt = i.CreatedAt,
                    UpdatedAt = i.UpdatedAt,
                    IsDeleted = i.IsDeleted
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (ingredient == null)
            {
                return Result<IngredientDTO>.ErrorResponse("Ingredient not found.");
            }
            return Result<IngredientDTO>.SuccessResponse(ingredient, "Ingredient retrieved successfully.");
        }
    }
}
