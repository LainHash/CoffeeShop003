using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Misc.Images.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Misc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Persistence.Repositories.Misc
{
    public class ImageRepository : IImageRepository
    {
        private readonly CoffeeShopDbContext _context;
        public ImageRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ImageDTO>>> GetImagesAsync()
        {
            var images = await _context.Images
                .Select(i => new ImageDTO
                {
                    ImageUrl = i.ImageUrl,
                    IsPrimary = i.IsPrimary,
                    CreatedAt = i.CreatedAt
                })
                .ToListAsync();
            return Result<List<ImageDTO>>
                .SuccessResponse(images);
        }
    }
}
