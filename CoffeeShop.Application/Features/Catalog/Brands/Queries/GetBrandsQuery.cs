using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Brands.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Brands.Queries
{
    public class GetBrandsQuery : IRequest<Result<List<BrandDTO>>>
    {
    }
}
