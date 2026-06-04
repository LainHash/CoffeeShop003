using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Products.Commands
{
    public class RestoreProductCommand : IRequest<Result<ProductDTO>>
    {
        public Guid Id { get; set; }
        public RestoreProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
