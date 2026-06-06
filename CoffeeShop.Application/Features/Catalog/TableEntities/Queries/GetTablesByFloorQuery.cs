using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.TableEntities.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.TableEntities.Queries
{
    public class GetTablesByFloorQuery : IRequest<Result<List<TableEntityDTO>>>
    {
        public int FloorNumber { get; set; }
        public GetTablesByFloorQuery(int floorNumber)
        {
            FloorNumber = floorNumber;
        }
    }
}
