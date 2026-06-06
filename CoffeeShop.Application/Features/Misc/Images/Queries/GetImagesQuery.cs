using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Misc.Images.DTOs;
using MediatR;

namespace CoffeeShop.Application.Features.Misc.Images.Queries
{
    public class GetImagesQuery : IRequest<Result<List<ImageDTO>>>
    {
    }
}
