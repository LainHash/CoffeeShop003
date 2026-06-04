using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Delete
{
    public class RestoreIngredientCommandHandler : IRequestHandler<RestoreIngredientCommand, Result>
    {
        private readonly IIngredientRepository _ingredientRepository;
        public RestoreIngredientCommandHandler(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<Result> Handle(RestoreIngredientCommand request, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.RestoreIngredientAsync(request.Id);
        }
    }
}
