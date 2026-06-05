using CoffeeShop.Application.Features.Catalog.TableEntities.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableEntityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TableEntityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableEntities()
        {
            var result = await _mediator.Send(new GetTableEntitiesQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{floor:int}")]
        public async Task<IActionResult> GetTablesByFloor([FromRoute] int floor)
        {
            var result = await _mediator.Send(new GetTablesByFloorQuery(floor));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{floor:int}/{table:int}")]
        public async Task<IActionResult> GetTableByNumber([FromRoute] int floor, [FromRoute] int table)
        {
            var result = await _mediator.Send(new GetTableByNumberQuery(floor, table));
            return StatusCode(result.StatusCode, result);
        }
    }
}
