using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Misc.Images.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Misc
{
    public interface IImageRepository
    {
        Task<Result<List<ImageDTO>>> GetImagesAsync();
    }
}
