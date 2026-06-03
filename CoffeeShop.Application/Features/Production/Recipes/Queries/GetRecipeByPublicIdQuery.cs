using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Production.Recipes.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Production.Recipes.Queries
{
    public class GetRecipeByPublicIdQuery : IRequest<Result<RecipeDTO>>
    {
        public Guid Id { get; set; }
        public GetRecipeByPublicIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
