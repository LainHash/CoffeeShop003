using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Products.Queries
{
    public class GetProductByPublicIdQueryHandler : IRequestHandler<GetProductByPublicIdQuery, Result<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByPublicIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductDTO>> Handle(GetProductByPublicIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetProductByPublicIdAsync(request.Id);
            if (!result.IsSuccess)
            {
                throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");
            }
            return result;
        }
    }
}
