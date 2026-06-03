

using CoffeeShop.Application.Features.Catalog.Ingredients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredients()
        {
            var result = await _mediator.Send(new GetIngredientsQuery());
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientByPublicId(Guid id)
        {
            var result = await _mediator.Send(new GetIngredientByPublicIdQuery(id));
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
