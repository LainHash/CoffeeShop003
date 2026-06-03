using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Categories.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<Result<List<CategoryDTO>>>
    {
    }
}
