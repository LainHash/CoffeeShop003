using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.TableEntities.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CoffeeShop.Infrastructure.Persistence.Repositories.Catalog
{
    public class TableEntityRepository : ITableEntityRepository
    {
        private readonly CoffeeShopDbContext _context;

        public TableEntityRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<TableEntityDTO>>> GetTablesAsync(CancellationToken cancellationToken)
        {
            var tables = await _context.TableEntities
                .Where(t => !t.IsDeleted)
                .Select(t => new TableEntityDTO
                {
                    PublicId = t.PublicId,
                    Shape = t.Shape,
                    TableNumber = t.TableNumber,
                    FloorNumber = t.FloorNumber,
                    Capacity = t.Capacity,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    IsDeleted = t.IsDeleted
                })
                .ToListAsync();
            return Result<List<TableEntityDTO>>
                .SuccessResponse(tables, "Tables retrieved successfully.", HttpStatusCode.OK);
        }

        public async Task<Result<List<TableEntityDTO>>> GetTablesByFloor(int floorNumber,
                                                                            CancellationToken cancellationToken)
        {
            var tables = await _context.TableEntities
                .Where(t => !t.IsDeleted && t.FloorNumber == floorNumber)
                .Select(t => new TableEntityDTO
                {
                    PublicId = t.PublicId,
                    Shape = t.Shape,
                    TableNumber = t.TableNumber,
                    FloorNumber = t.FloorNumber,
                    Capacity = t.Capacity,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    IsDeleted = t.IsDeleted
                })
                .ToListAsync();
            return Result<List<TableEntityDTO>>
                .SuccessResponse(tables, "Tables retrieved successfully.", HttpStatusCode.OK);
        }

        public async Task<Result<TableEntityDTO>> GetTableByNumber(int floorNumber, int tableNumber,
                                                                    CancellationToken cancellationToken)
        {
            var table = await _context.TableEntities
                .Where(t => !t.IsDeleted && t.FloorNumber == floorNumber && t.TableNumber == tableNumber)
                .Select(t => new TableEntityDTO
                {
                    PublicId = t.PublicId,
                    Shape = t.Shape,
                    TableNumber = t.TableNumber,
                    FloorNumber = t.FloorNumber,
                    Capacity = t.Capacity,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    IsDeleted = t.IsDeleted
                })
                .FirstOrDefaultAsync();
            if (table == null)
            {
                return Result<TableEntityDTO>
                    .ErrorResponse("Table not found.", HttpStatusCode.NotFound);
            }
            return Result<TableEntityDTO>
                .SuccessResponse(table, "Table retrieved successfully.", HttpStatusCode.OK);
        }


    }
}
