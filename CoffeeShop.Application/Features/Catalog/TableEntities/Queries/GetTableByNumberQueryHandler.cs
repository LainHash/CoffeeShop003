using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.TableEntities.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.TableEntities.Queries
{
    public class GetTableByNumberQueryHandler : IRequestHandler<GetTableByNumberQuery, Result<TableEntityDTO>>
    {
        private readonly ITableEntityRepository _tableEntityRepository;
        public GetTableByNumberQueryHandler(ITableEntityRepository tableEntityRepository)
        {
            _tableEntityRepository = tableEntityRepository;
        }
        public async Task<Result<TableEntityDTO>> Handle(GetTableByNumberQuery request, CancellationToken cancellationToken)
        {
            return await _tableEntityRepository.GetTableByNumber(request.FloorNumber, request.TableNumber, cancellationToken);
        }
    }
}
