using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.API.Controllers.Misc
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var result = await _mediator.Send(new Application.Features.Misc.Images.Queries.GetImagesQuery());
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
