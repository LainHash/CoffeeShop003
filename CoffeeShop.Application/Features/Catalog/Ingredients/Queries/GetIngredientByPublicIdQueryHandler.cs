using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Queries
{
    public class GetIngredientByPublicIdQueryHandler : IRequestHandler<GetIngredientByPublicIdQuery, Result<IngredientDTO>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        public GetIngredientByPublicIdQueryHandler(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<Result<IngredientDTO>> Handle(GetIngredientByPublicIdQuery request, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.GetIngredientByPublicIdAsync(request.Id, cancellationToken);
        }
    }
}
