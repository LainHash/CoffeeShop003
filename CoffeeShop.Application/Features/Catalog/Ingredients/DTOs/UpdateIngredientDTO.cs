
namespace CoffeeShop.Application.Features.Catalog.Ingredients.DTOs
{
    public class UpdateIngredientDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string BrandName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Stock { get; set; }
    }
}
