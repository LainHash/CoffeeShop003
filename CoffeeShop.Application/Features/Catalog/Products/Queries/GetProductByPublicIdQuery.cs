using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Products.Queries
{
    public class GetProductByPublicIdQuery : IRequest<Result<ProductDTO>>
    {
        public Guid Id { get; set; }

        public GetProductByPublicIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
