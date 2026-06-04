using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Production.Recipes.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Production;
using MediatR;

namespace CoffeeShop.Application.Features.Production.Recipes.Queries
{
    public class GetRecipeByPublicIdQueryHandler : IRequestHandler<GetRecipeByPublicIdQuery, Result<RecipeDTO>>
    {
        private readonly IRecipeRepository _recipeRepository;
        public GetRecipeByPublicIdQueryHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task<Result<RecipeDTO>> Handle(GetRecipeByPublicIdQuery request, CancellationToken cancellationToken)
        {
            return await _recipeRepository.GetRecipeByPublicIdAsync(request.Id, cancellationToken);
        }
    }
}
