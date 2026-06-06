
using CoffeeShop.Application.Features.Catalog.Products.Commands.Create;
using CoffeeShop.Application.Features.Catalog.Products.Commands.Delete;
using CoffeeShop.Application.Features.Catalog.Products.Commands.Update;
using CoffeeShop.Application.Features.Catalog.Products.DTOs;
using CoffeeShop.Application.Features.Catalog.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByPublicId([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetProductByPublicIdQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            var result = await _mediator.Send(new CreateProductCommand(createProductDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDTO updateProductDTO)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, updateProductDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> RestoreProduct([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new RestoreProductCommand(id));
            return StatusCode(result.StatusCode, result);
        }
    }
}
