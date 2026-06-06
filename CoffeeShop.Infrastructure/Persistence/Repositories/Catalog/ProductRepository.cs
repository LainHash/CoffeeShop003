using CoffeeShop.Application.Common.Constants;
using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using CoffeeShop.Application.Features.Misc.Images.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using CoffeeShop.Domain.Entities.Catalog;
using CoffeeShop.Domain.Entities.Inventory;
using CoffeeShop.Domain.Entities.Misc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
                p => p.Id,
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
                    Name = p.Product.Name,
                    Description = p.Product.Description,
                    IsMadeToOrder = p.Product.IsMadeToOrder,
                    BrandName = p.Product.Brand != null ? p.Product.Brand.Name : "",
                    CategoryName = p.Product.Category.Name,
                    Images = new List<ImageDTO>
                    {
                        new ImageDTO
                        {
                            ImageUrl = p.Image.ImageUrl,
                            IsPrimary = p.Image.IsPrimary,
                            CreatedAt = p.Image.CreatedAt
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
            return Result<List<ProductDTO>>
                    .SuccessResponse(products, "Products retrieved successfully.", HttpStatusCode.OK);
        }

        public async Task<Result<ProductDTO>> GetProductByPublicIdAsync(Guid id,
                                                                        CancellationToken cancellationToken = default)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSku)
                .Join(_context.Images,
                p => p.Id,
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
                    Name = p.Product.Name,
                    Description = p.Product.Description,
                    IsMadeToOrder = p.Product.IsMadeToOrder,
                    BrandName = p.Product.Brand != null ? p.Product.Brand.Name : string.Empty,
                    CategoryName = p.Product.Category.Name,
                    Images = new List<ImageDTO>
                    {
                        new ImageDTO
                        {
                            ImageUrl = p.Image.ImageUrl,
                            IsPrimary = p.Image.IsPrimary,
                            CreatedAt = p.Image.CreatedAt
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
                return Result<ProductDTO>
                        .ErrorResponse("Product not found.", HttpStatusCode.NotFound);
            }
            return Result<ProductDTO>
                    .SuccessResponse(product, "Product retrieved successfully.", HttpStatusCode.OK);

        }

        public async Task<Result<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO,
                                                                    CancellationToken cancellationToken = default)
        {
            using var transacton = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var product = new Product()
                {
                    Name = createProductDTO.Name,
                    Description = createProductDTO.Description ?? string.Empty,
                    IsMadeToOrder = createProductDTO.IsMadeToOrder,
                    CategoryId = await GetCategoryId(createProductDTO.CategoryName),
                    BrandId = await GetBrandId(createProductDTO.BrandName),
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync(cancellationToken);

                var productSku = new ProductSku()
                {
                    ProductId = product.Id,
                    UnitPrice = createProductDTO.UnitPrice,
                    Unit = createProductDTO.Unit,
                    Stock = createProductDTO.Stock,
                    Status = StockStatus.Active,
                };
                _context.ProductSkus.Add(productSku);
                await _context.SaveChangesAsync(cancellationToken);

                var images = createProductDTO.Images.Select(i => new Image
                {
                    ReferenceId = product.Id,
                    Type = ReferenceType.Product,
                    ImageUrl = i.ImageUrl,
                    IsPrimary = i.IsPrimary
                }).ToList();
                _context.Images.AddRange(images);
                await _context.SaveChangesAsync(cancellationToken);

                await transacton.CommitAsync(cancellationToken);
                var productDTO = new ProductDTO()
                {
                    PublicId = product.PublicId,
                    Name = product.Name,
                    Description = product.Description,
                    IsMadeToOrder = product.IsMadeToOrder,
                    BrandName = createProductDTO.BrandName ?? string.Empty,
                    CategoryName = createProductDTO.CategoryName,
                    Images = createProductDTO.Images.Select(i => new ImageDTO
                    {
                        ImageUrl = i.ImageUrl,
                        IsPrimary = i.IsPrimary
                    }).ToList(),
                    UnitPrice = productSku.UnitPrice,
                    Unit = productSku.Unit,
                    Stock = productSku.Stock,
                    Status = productSku.Status,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                    IsDeleted = product.IsDeleted
                };
                return Result<ProductDTO>
                    .SuccessResponse(productDTO, "Product created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transacton.RollbackAsync(cancellationToken);
                return Result<ProductDTO>
                    .ErrorResponse($"An error occurred while creating the product: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<ProductDTO>> UpdateProductAsync(Guid id, UpdateProductDTO updateProductDTO,
                                                                    CancellationToken cancellationToken = default)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductSku)
                .Join(_context.Images,
                p => p.Id,
                i => i.ReferenceId,
                (p, i) => new
                {
                    Product = p,
                    Image = i
                })
                .Where(x => x.Product.PublicId == id && x.Image.Type == ReferenceType.Product);

            var product = await query.FirstOrDefaultAsync(cancellationToken);
            if (product == null)
            {
                return Result<ProductDTO>
                        .ErrorResponse("Product not found.", HttpStatusCode.NotFound);
            }

            product.Product.Name = updateProductDTO.Name;
            product.Product.Description = updateProductDTO.Description ?? string.Empty;
            product.Product.IsMadeToOrder = updateProductDTO.IsMadeToOrder;

            product.Product.CategoryId = await GetCategoryId(updateProductDTO.CategoryName);
            product.Product.BrandId = await GetBrandId(updateProductDTO.BrandName);

            product.Product.ProductSku.UnitPrice = updateProductDTO.UnitPrice;
            product.Product.ProductSku.Unit = updateProductDTO.Unit;
            product.Product.ProductSku.Stock = updateProductDTO.Stock;

            product.Product.UpdatedAt = DateTime.Now;

            var dto = new ProductDTO
            {
                PublicId = product.Product.PublicId,
                Name = product.Product.Name,
                Description = product.Product.Description,
                IsMadeToOrder = product.Product.IsMadeToOrder,
                BrandName = product.Product.Brand != null ? product.Product.Brand.Name : string.Empty,
                CategoryName = product.Product.Category.Name,
                Images = new List<ImageDTO>
                {
                    new ImageDTO
                    {
                        ImageUrl = product.Image.ImageUrl,
                        IsPrimary = product.Image.IsPrimary,
                        CreatedAt = product.Image.CreatedAt
                    }
                },
                UnitPrice = product.Product.ProductSku.UnitPrice,
                Unit = product.Product.ProductSku.Unit,
                Stock = product.Product.ProductSku.Stock,
                Status = product.Product.ProductSku.Status,
                CreatedAt = product.Product.CreatedAt,
                UpdatedAt = product.Product.UpdatedAt,
                IsDeleted = product.Product.IsDeleted
            };
            await _context.SaveChangesAsync(cancellationToken);
            return Result<ProductDTO>
                    .SuccessResponse(dto, "Product updated successfully.", HttpStatusCode.OK);
        }

        public async Task<Result> DeleteProductAsync(Guid id,
                                                    CancellationToken cancellationToken = default)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.PublicId == id, cancellationToken);
            if (product == null)
            {
                return Result
                        .ErrorResponse("Product not found.", HttpStatusCode.NotFound);
            }
            if (product.IsDeleted)
            {
                return Result
                        .ErrorResponse("Product is already deleted.", HttpStatusCode.Conflict);
            }

            product.IsDeleted = true;
            product.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Result
                    .SuccessResponse("Product deleted successfully.", HttpStatusCode.OK);
        }

        public async Task<Result> RestoreProductAsync(Guid id,
                                                    CancellationToken cancellationToken = default)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.PublicId == id, cancellationToken);

            if (product == null)
            {
                return Result
                        .ErrorResponse("Product not found.", HttpStatusCode.NotFound);
            }
            if (!product.IsDeleted)
            {
                return Result
                        .ErrorResponse("Product is not deleted.", HttpStatusCode.Conflict);
            }

            product.IsDeleted = false;
            product.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Result
                    .SuccessResponse("Product restored successfully.", HttpStatusCode.OK);
        }

        private async Task<int> GetCategoryId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Category name cannot be null or empty.");
            }
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
            if (category == null)
            {
                var newCategory = new Category { Name = name };
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
                return newCategory.Id;
            }
            return category.Id;
        }

        private async Task<int?> GetBrandId(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Name == name);
            if (brand == null)
            {
                var newBrand = new Brand { Name = name };
                _context.Brands.Add(newBrand);
                await _context.SaveChangesAsync();
                return newBrand.Id;
            }
            return brand.Id;
        }


    }
}
