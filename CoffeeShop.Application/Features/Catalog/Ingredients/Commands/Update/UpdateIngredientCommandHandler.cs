using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Update
{
    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, Result<IngredientDTO>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        public UpdateIngredientCommandHandler(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Result<IngredientDTO>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.UpdateIngredientAsync(request.Id, request.UpdateIngredientDTO);
        }
    }
}
