using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Catalog
{
    public interface IProductRepository
    {
        Task<Result<List<ProductDTO>>> GetProductsAsync();
        Task<Result<ProductDTO>> GetProductByPublicIdAsync(Guid publicId);
    }
}
