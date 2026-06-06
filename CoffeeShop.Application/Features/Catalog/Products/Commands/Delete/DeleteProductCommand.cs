using CoffeeShop.Application.Common.Models;
using MediatR;

namespace CoffeeShop.Application.Features.Catalog.Products.Commands.Delete
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
