using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.TableEntities.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Catalog
{
    public interface ITableEntityRepository
    {
        Task<Result<List<TableEntityDTO>>> GetTablesAsync(CancellationToken cancellationToken);
        Task<Result<List<TableEntityDTO>>> GetTablesByFloor(int floorNumber,
                                                            CancellationToken cancellationToken);
        Task<Result<TableEntityDTO>> GetTableByNumber(int floorNumber, int tableNumber,
                                                        CancellationToken cancellationToken);

    }
}
