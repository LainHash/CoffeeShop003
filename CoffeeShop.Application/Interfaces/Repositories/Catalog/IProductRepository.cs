using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Catalog
{
    public interface IProductRepository
    {
        Task<Result<List<ProductDTO>>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<Result<ProductDTO>> GetProductByPublicIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
