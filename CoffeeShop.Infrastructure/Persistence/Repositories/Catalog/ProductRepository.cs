using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Persistence.Repositories.Catalog
{
    public class ProductRepository : IProductRepository
    {
        private readonly CoffeeShopDbContext _context;
        public ProductRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ProductDTO>>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSku)
                .Select(p => new ProductDTO
                {
                    PublicId = p.PublicId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    IsMadeToOrder = p.IsMadeToOrder,
                    BrandName = p.Brand != null ? p.Brand.BrandName : "",
                    CategoryName = p.Category.CategoryName,
                    UnitPrice = p.ProductSku.UnitPrice,
                    Unit = p.ProductSku.Unit,
                    Stock = p.ProductSku.Stock,
                    Status = p.ProductSku.Status,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsDeleted = p.IsDeleted
                })
                .ToListAsync(cancellationToken);
            return Result<List<ProductDTO>>.SuccessResponse(products, "Products retrieved successfully.");
        }

        public async Task<Result<ProductDTO>> GetProductByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSku)
                .Where(p => p.PublicId == publicId)
                .Select(p => new ProductDTO
                {
                    PublicId = p.PublicId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    IsMadeToOrder = p.IsMadeToOrder,
                    BrandName = p.Brand != null ? p.Brand.BrandName : string.Empty,
                    CategoryName = p.Category.CategoryName,
                    UnitPrice = p.ProductSku.UnitPrice,
                    Unit = p.ProductSku.Unit,
                    Stock = p.ProductSku.Stock,
                    Status = p.ProductSku.Status,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsDeleted = p.IsDeleted
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (product == null)
            {
                return Result<ProductDTO>.ErrorResponse("Product not found.");
            }
            return Result<ProductDTO>.SuccessResponse(product, "Product retrieved successfully.");

        }
    }
}
