using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Production.Recipes.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Production;
using MediatR;

namespace CoffeeShop.Application.Features.Production.Recipes.Queries
{
    public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, Result<List<RecipeDTO>>>
    {
        private readonly IRecipeRepository _recipeRepository;
        public GetRecipesQueryHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task<Result<List<RecipeDTO>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
        {
            return await _recipeRepository.GetRecipesAsync();
        }
    }
}
