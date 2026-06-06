

using CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Create;
using CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Delete;
using CoffeeShop.Application.Features.Catalog.Ingredients.Commands.Update;
using CoffeeShop.Application.Features.Catalog.Ingredients.DTOs;
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
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientByPublicId(Guid id)
        {
            var result = await _mediator.Send(new GetIngredientByPublicIdQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDTO createIngredientDTO)
        {
            var result = await _mediator.Send(new CreateIngredientCommand(createIngredientDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] UpdateIngredientDTO updateIngredientDTO)
        {
            var result = await _mediator.Send(new UpdateIngredientCommand(id, updateIngredientDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var result = await _mediator.Send(new DeleteIngredientCommand(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> RestoreIngredient(Guid id)
        {
            var result = await _mediator.Send(new RestoreIngredientCommand(id));
            return StatusCode(result.StatusCode, result);
        }
    }
}
