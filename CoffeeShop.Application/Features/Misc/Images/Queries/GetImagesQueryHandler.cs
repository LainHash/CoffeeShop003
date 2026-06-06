using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Misc.Images.DTOs;
using CoffeeShop.Application.Interfaces.Repositories.Misc;
using MediatR;

namespace CoffeeShop.Application.Features.Misc.Images.Queries
{
    public class GetImagesQueryHandler : IRequestHandler<GetImagesQuery, Result<List<ImageDTO>>>
    {
        private readonly IImageRepository _imageRepository;
        public GetImagesQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<Result<List<ImageDTO>>> Handle(GetImagesQuery request, CancellationToken cancellationToken)
        {
            return await _imageRepository.GetImagesAsync();
        }
    }
}
