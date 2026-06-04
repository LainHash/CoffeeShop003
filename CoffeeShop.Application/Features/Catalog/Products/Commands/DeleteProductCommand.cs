using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Products.Commands
{
    public class DeleteProductCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
