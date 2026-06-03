using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.Queries
{
    public class GetIngredientsQuery : IRequest<Result<List<IngredientDTO>>>
    {

    }
}
