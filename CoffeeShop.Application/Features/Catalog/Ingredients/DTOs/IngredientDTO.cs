namespace CoffeeShop.Application.Features.Catalog.Ingredients.DTOs
{
    public class IngredientDTO
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string IngredientName { get; set; } = null!;
        public string? Description { get; set; }

        public string BrandName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Stock { get; set; }
        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
