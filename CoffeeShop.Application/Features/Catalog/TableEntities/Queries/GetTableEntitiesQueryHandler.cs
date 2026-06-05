using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.TableEntities.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.TableEntities.Queries
{
    public class GetTableEntitiesQueryHandler : IRequestHandler<GetTableEntitiesQuery, Result<List<TableEntityDTO>>>
    {
        private readonly ITableEntityRepository _tableEntityRepository;
        public GetTableEntitiesQueryHandler(ITableEntityRepository tableEntityRepository)
        {
            _tableEntityRepository = tableEntityRepository;
        }

        public async Task<Result<List<TableEntityDTO>>> Handle(GetTableEntitiesQuery request, CancellationToken cancellationToken)
        {
            return await _tableEntityRepository.GetTablesAsync(cancellationToken);
        }
    }
}
