using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Products.Queries
{
    public class GetProductsQuery : IRequest<Result<List<ProductDTO>>>
    {
        public GetProductsQuery() { }
    }
}
