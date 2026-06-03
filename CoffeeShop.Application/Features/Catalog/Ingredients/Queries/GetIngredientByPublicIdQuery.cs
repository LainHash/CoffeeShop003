using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Queries
{
    public class GetIngredientByPublicIdQuery : IRequest<Result<IngredientDTO>>
    {
        public Guid Id { get; }

        public GetIngredientByPublicIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
