using CoffeeShop.Application.Common.Models;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Delete
{
    public class DeleteIngredientCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public DeleteIngredientCommand(Guid id)
        {
            Id = id;
        }
    }
}
