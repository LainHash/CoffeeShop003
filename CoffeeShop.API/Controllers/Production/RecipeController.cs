using CoffeeShop.Application.Features.Production.Recipes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.API.Controllers.Production
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var result = await _mediator.Send(new GetRecipesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(Guid id)
        {
            var result = await _mediator.Send(new GetRecipeByPublicIdQuery(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
