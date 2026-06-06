using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Create
{
    public class CreateIngredientCommand : IRequest<Result<IngredientDTO>>
    {
        public CreateIngredientDTO CreateIngredientDTO { get; set; } = null!;
        public CreateIngredientCommand(CreateIngredientDTO createIngredientDTO)
        {
            CreateIngredientDTO = createIngredientDTO;
        }
    }
}
