using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Products.Commands.Delete
{
    public class RestoreProductCommandHandler : IRequestHandler<RestoreProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        public RestoreProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(RestoreProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.RestoreProductAsync(request.Id, cancellationToken);
            return result;
        }
    }
}
