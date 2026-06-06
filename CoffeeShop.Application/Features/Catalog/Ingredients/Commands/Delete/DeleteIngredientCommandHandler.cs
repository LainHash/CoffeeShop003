using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Delete
{
    public class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, Result>
    {
        private readonly IIngredientRepository _ingredientRepository;
        public DeleteIngredientCommandHandler(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<Result> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            return await _ingredientRepository.DeleteIngredientAsync(request.Id);
        }
    }
}
