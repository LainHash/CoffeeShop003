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

        public async Task<Result<List<ProductDTO>>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Select(p => new ProductDTO
                {
                    PublicId = p.PublicId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    IsMadeToOrder = p.IsMadeToOrder,
                    BrandName = p.Brand != null ? p.Brand.BrandName : string.Empty,
                    CategoryName = p.Category.CategoryName,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsDeleted = p.IsDeleted
                })
                .ToListAsync();
            return new Result<List<ProductDTO>>
            {
                IsSuccess = true,
                Message = "Products retrieved successfully.",
                Data = products
            };
        }

        public async Task<Result<ProductDTO>> GetProductByPublicIdAsync(Guid publicId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Select(p => new ProductDTO
                {
                    PublicId = p.PublicId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    IsMadeToOrder = p.IsMadeToOrder,
                    BrandName = p.Brand != null ? p.Brand.BrandName : string.Empty,
                    CategoryName = p.Category.CategoryName,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    IsDeleted = p.IsDeleted
                })
                .FirstOrDefaultAsync(p => p.PublicId == publicId);
            if(product == null)
            {
                return new Result<ProductDTO>
                {
                    IsSuccess = false,
                    Message = "Product not found.",
                    Data = null
                };
            }
            return new Result<ProductDTO>
            {
                IsSuccess = true,
                Message = "Product retrieved successfully.",
                Data = product
            };
        }
    }
}
