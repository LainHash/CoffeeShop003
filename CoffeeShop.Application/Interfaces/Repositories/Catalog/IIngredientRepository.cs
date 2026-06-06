using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Catalog
{
    public interface IIngredientRepository
    {
        Task<Result<List<IngredientDTO>>> GetIngredientsAsync(CancellationToken cancellationToken = default);
        Task<Result<IngredientDTO>> GetIngredientByPublicIdAsync(Guid publicId,
                                                                    CancellationToken cancellationToken = default);

        Task<Result<IngredientDTO>> CreateIngredientAsync(CreateIngredientDTO createIngredientDTO,
                                                            CancellationToken cancellationToken = default);

        Task<Result<IngredientDTO>> UpdateIngredientAsync(Guid publicId, UpdateIngredientDTO updateIngredientDTO,
                                                            CancellationToken cancellationToken = default);

        Task<Result> DeleteIngredientAsync(Guid publicId,
                                            CancellationToken cancellationToken = default);
        Task<Result> RestoreIngredientAsync(Guid publicId,
                                            CancellationToken cancellationToken = default);
    }
}
