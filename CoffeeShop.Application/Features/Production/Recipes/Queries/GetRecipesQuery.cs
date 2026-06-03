using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Production.Recipes.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Production.Recipes.Queries
{
    public class GetRecipesQuery : IRequest<Result<List<RecipeDTO>>>
    {
    }
}
