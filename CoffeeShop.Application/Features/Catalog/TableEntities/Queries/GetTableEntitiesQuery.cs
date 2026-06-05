using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.TableEntities.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.TableEntities.Queries
{
    public class GetTableEntitiesQuery : IRequest<Result<List<TableEntityDTO>>>
    {
    }
}
