using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using CoffeeShop.Application.Features.Misc.Images.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using CoffeeShop.Domain.Common.Constants;
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
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSku)
                .Join(_context.Images,
                p => p.ProductId,
                i => i.ReferenceId,
                (p, i) => new
                {
                    Product = p,
                    Image = i
                })
                .Where(x => x.Image.Type == ReferenceType.Product);

            var products = await query
                .Select(p => new ProductDTO
                {
                    PublicId = p.Product.PublicId,
                    ProductName = p.Product.ProductName,
                    Description = p.Product.Description,
                    IsMadeToOrder = p.Product.IsMadeToOrder,
                    BrandName = p.Product.Brand != null ? p.Product.Brand.BrandName : "",
                    CategoryName = p.Product.Category.CategoryName,
                    Images = new List<ImageDTO>
                    {
                        new ImageDTO
                        {
                            ImageUrl = p.Image.ImageUrl,
                            IsPrimary = p.Image.IsPrimary
                        }
                    },
                    UnitPrice = p.Product.ProductSku.UnitPrice,
                    Unit = p.Product.ProductSku.Unit,
                    Stock = p.Product.ProductSku.Stock,
                    Status = p.Product.ProductSku.Status,
                    CreatedAt = p.Product.CreatedAt,
                    UpdatedAt = p.Product.UpdatedAt,
                    IsDeleted = p.Product.IsDeleted
                })
                .ToListAsync(cancellationToken);
            return Result<List<ProductDTO>>.SuccessResponse(products, "Products retrieved successfully.");
        }

        public async Task<Result<ProductDTO>> GetProductByPublicIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSku)
                .Join(_context.Images,
                p => p.ProductId,
                i => i.ReferenceId,
                (p, i) => new
                {
                    Product = p,
                    Image = i
                })
                .Where(x => x.Product.PublicId == id && x.Image.Type == ReferenceType.Product);

            var product = await query
                .Select(p => new ProductDTO
                {
                    PublicId = p.Product.PublicId,
                    ProductName = p.Product.ProductName,
                    Description = p.Product.Description,
                    IsMadeToOrder = p.Product.IsMadeToOrder,
                    BrandName = p.Product.Brand != null ? p.Product.Brand.BrandName : string.Empty,
                    CategoryName = p.Product.Category.CategoryName,
                    Images = new List<ImageDTO>
                    {
                        new ImageDTO
                        {
                            ImageUrl = p.Image.ImageUrl,
                            IsPrimary = p.Image.IsPrimary
                        }
                    },
                    UnitPrice = p.Product.ProductSku.UnitPrice,
                    Unit = p.Product.ProductSku.Unit,
                    Stock = p.Product.ProductSku.Stock,
                    Status = p.Product.ProductSku.Status,
                    CreatedAt = p.Product.CreatedAt,
                    UpdatedAt = p.Product.UpdatedAt,
                    IsDeleted = p.Product.IsDeleted
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
