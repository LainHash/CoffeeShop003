using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Catalog
{
    public interface IIngredientRepository
    {
        Task<Result<List<IngredientDTO>>> GetIngredientsAsync();
        Task<Result<IngredientDTO>> GetIngredientByPublicIdAsync(Guid publicId);
    }
}
