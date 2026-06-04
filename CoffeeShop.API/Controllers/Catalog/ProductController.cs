using CoffeeShop.Application.Features.Catalog.Products.Commands;
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
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByPublicId([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetProductByPublicIdQuery(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            var result = await _mediator.Send(new CreateProductCommand(createProductDTO));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDTO updateProductDTO)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, updateProductDTO));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> RestoreProduct([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new RestoreProductCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
