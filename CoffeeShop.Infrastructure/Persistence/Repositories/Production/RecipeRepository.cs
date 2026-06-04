using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Production.Recipes.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Production;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Persistence.Repositories.Production
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CoffeeShopDbContext _context;
        public RecipeRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<RecipeDTO>>> GetRecipesAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.Recipes
                .Include(r => r.Product)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                        .ThenInclude(i => i.IngredientSku)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                        .ThenInclude(i => i.Brand)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                        .ThenInclude(i => i.Category)
                .Include(r => r.RecipeSteps)
                .Where(r => !r.IsDeleted);
            var recipes = await query
                .Select(r => new RecipeDTO
                {
                    PublicId = r.PublicId,
                    Inspiration = r.Inspiration,
                    Description = r.Description,
                    ProductName = r.Product.ProductName,
                    Ingredients = r.RecipeIngredients.Select(ri => new RecipeIngredient
                    {
                        IngredientName = ri.Ingredient.IngredientName,
                        Quantity = ri.Quantity,
                        Unit = ri.Ingredient.IngredientSku.Unit,
                        BrandName = ri.Ingredient.Brand.BrandName,
                        CategoryName = ri.Ingredient.Category.CategoryName
                    }).ToList(),
                    Steps = r.RecipeSteps.Select(rs => new RecipeStep
                    {
                        StepNumber = rs.StepNumber,
                        Description = rs.Description,
                        DurationSeconds = rs.DurationSeconds
                    }).ToList(),
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    IsDeleted = r.IsDeleted
                })
                .ToListAsync(cancellationToken);
            return Result<List<RecipeDTO>>.SuccessResponse(recipes, "Recipes retrieved successfully.");
        }

        public async Task<Result<RecipeDTO>> GetRecipeByPublicIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = _context.Recipes
                .Include(r => r.Product)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                        .ThenInclude(i => i.IngredientSku)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                        .ThenInclude(i => i.Brand)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                        .ThenInclude(i => i.Category)
                .Include(r => r.RecipeSteps)
                .Where(r => !r.IsDeleted && r.PublicId == id);
            var recipe = await query
                .Select(r => new RecipeDTO
                {
                    PublicId = r.PublicId,
                    Inspiration = r.Inspiration,
                    Description = r.Description,
                    ProductName = r.Product.ProductName,
                    Ingredients = r.RecipeIngredients.Select(ri => new RecipeIngredient
                    {
                        IngredientName = ri.Ingredient.IngredientName,
                        Quantity = ri.Quantity,
                        Unit = ri.Ingredient.IngredientSku.Unit,
                        BrandName = ri.Ingredient.Brand.BrandName,
                        CategoryName = ri.Ingredient.Category.CategoryName
                    }).ToList(),
                    Steps = r.RecipeSteps.Select(rs => new RecipeStep
                    {
                        StepNumber = rs.StepNumber,
                        Description = rs.Description,
                        DurationSeconds = rs.DurationSeconds
                    }).ToList(),
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    IsDeleted = r.IsDeleted
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (recipe == null)
            {
                return Result<RecipeDTO>.ErrorResponse("Recipe not found.");
            }
            return Result<RecipeDTO>.SuccessResponse(recipe, "Recipe retrieved successfully.");
        }
    }
}
