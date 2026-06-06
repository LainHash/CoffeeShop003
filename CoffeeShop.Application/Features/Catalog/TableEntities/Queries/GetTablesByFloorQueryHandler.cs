using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.TableEntities.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.TableEntities.Queries
{
    public class GetTablesByFloorQueryHandler : IRequestHandler<GetTablesByFloorQuery, Result<List<TableEntityDTO>>>
    {
        private readonly ITableEntityRepository _tableEntityRepository;
        public GetTablesByFloorQueryHandler(ITableEntityRepository tableEntityRepository)
        {
            _tableEntityRepository = tableEntityRepository;
        }

        public async Task<Result<List<TableEntityDTO>>> Handle(GetTablesByFloorQuery request, CancellationToken cancellationToken)
        {
            return await _tableEntityRepository.GetTablesByFloor(request.FloorNumber, cancellationToken);
        }
    }
}
