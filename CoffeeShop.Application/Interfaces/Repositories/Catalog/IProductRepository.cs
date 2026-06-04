using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Catalog
{
    public interface IProductRepository
    {
        Task<Result<List<ProductDTO>>> GetProductsAsync(CancellationToken cancellationToken = default);

        Task<Result<ProductDTO>> GetProductByPublicIdAsync(Guid id,
                                                            CancellationToken cancellationToken = default);

        Task<Result<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO,
                                                    CancellationToken cancellationToken = default);

        Task<Result<ProductDTO>> UpdateProductAsync(Guid id, UpdateProductDTO updateProductDTO,
                                                    CancellationToken cancellationToken = default);

        Task<Result> DeleteProductAsync(Guid id, 
                                        CancellationToken cancellationToken = default);

        Task<Result> RestoreProductAsync(Guid id, 
                                        CancellationToken cancellationToken = default);

    }
}
