using CoffeeShop.Application.Common.Models;
using CoffeeShop.Application.Features.Catalog.Categories.DTOs;

namespace CoffeeShop.Application.Interfaces.Repositories.Catalog
{
    public interface ICategoryRepository
    {
        Task<Result<List<CategoryDTO>>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    }
}
