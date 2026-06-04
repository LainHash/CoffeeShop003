using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Create
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Result<IngredientDTO>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        public CreateIngredientCommandHandler(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<Result<IngredientDTO>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.CreateIngredientAsync(request.CreateIngredientDTO, cancellationToken);
        }
    }
}
