using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Queries
{
    public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, Result<List<IngredientDTO>>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        public GetIngredientsQueryHandler(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<Result<List<IngredientDTO>>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.GetIngredientsAsync(cancellationToken);
        }
    }
}
