namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public string? Description { get; set; }

        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
