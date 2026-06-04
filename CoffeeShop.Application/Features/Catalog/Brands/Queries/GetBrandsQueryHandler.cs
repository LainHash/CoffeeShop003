using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Brands.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Brands.Queries
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, Result<List<BrandDTO>>>
    {
        private readonly IBrandRepository _brandRepository;
        public GetBrandsQueryHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<Result<List<BrandDTO>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            return await _brandRepository.GetBrandsAsync(cancellationToken);
        }
    }
}
