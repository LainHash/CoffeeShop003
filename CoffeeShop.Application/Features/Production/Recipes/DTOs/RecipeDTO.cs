namespace CoffeeShop.Application.Features.Production.Recipes.DTOs
{
    public class RecipeDTO
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string Inspiration { get; set; } = null!;
        public string? Description { get; set; }
        public string ProductName { get; set; } = null!;

        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
        public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }


    }

    public class RecipeIngredient
    {
        public string IngredientName { get; set; } = null!;
        public int Quantity { get; set; }

        public string BrandName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;

        public string Unit { get; set; } = null!;
    }

    public class RecipeStep
    {
        public int StepNumber { get; set; }
        public string Description { get; set; } = null!;
        public int DurationSeconds { get; set; }
    }
}
