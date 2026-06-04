using CoffeeShop.Application.Common.Models;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Delete
{
    public class RestoreIngredientCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public RestoreIngredientCommand(Guid id)
        {
            Id = id;
        }
    }
}
