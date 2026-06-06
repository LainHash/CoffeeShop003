using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Update
{
    public class UpdateIngredientCommand : IRequest<Result<IngredientDTO>>
    {
        public Guid Id { get; set; }
        public UpdateIngredientDTO UpdateIngredientDTO { get; set; } = null!;
        public UpdateIngredientCommand(Guid id, UpdateIngredientDTO updateIngredientDTO)
        {
            Id = id;
            UpdateIngredientDTO = updateIngredientDTO;
        }
    }
}
