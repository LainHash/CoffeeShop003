using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.TableEntities.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.TableEntities.Queries
{
    public class GetTableByNumberQuery : IRequest<Result<TableEntityDTO>>
    {
        public int FloorNumber { get; set; }
        public int TableNumber { get; set; }
        public GetTableByNumberQuery(int floorNumber, int tableNumber)
        {
            FloorNumber = floorNumber;
            TableNumber = tableNumber;
        }
    }
}
