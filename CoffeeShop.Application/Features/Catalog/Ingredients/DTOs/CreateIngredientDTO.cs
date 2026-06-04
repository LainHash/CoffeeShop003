using CoffeeShop.Application.Features.Misc.Images.DTOs;

namespace CoffeeShop.Application.Features.Catalog.Ingredients.DTOs
{
    public class CreateIngredientDTO
    {
        public string IngredientName { get; set; } = null!;
        public string? Description { get; set; }

        public string BrandName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public List<ImageDTO> Images { get; set; } = new List<ImageDTO>();

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Stock { get; set; }
    }
}
