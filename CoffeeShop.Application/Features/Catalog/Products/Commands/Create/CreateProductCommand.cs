using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<Result<ProductDTO>>
    {
        public CreateProductDTO CreateProductDTO { get; set; } = null!;
        public CreateProductCommand(CreateProductDTO createProductDTO)
        {
            CreateProductDTO = createProductDTO;
        }
    }
}
