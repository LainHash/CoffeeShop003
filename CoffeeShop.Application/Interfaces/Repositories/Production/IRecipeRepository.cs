using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Production.Recipes.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Production
{
    public interface IRecipeRepository
    {
        Task<Result<List<RecipeDTO>>> GetRecipesAsync();
        Task<Result<RecipeDTO>> GetRecipeByPublicIdAsync(Guid publicId);
    }
}
