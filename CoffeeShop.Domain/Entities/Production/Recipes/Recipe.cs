using CoffeeShop.Domain.Entities.Catalog;

namespace CoffeeShop.Domain.Entities.Production.Recipes
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string Inspiration { get; set; } = null!;
        public string? Description { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        // Foreign Keys
        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();
    }
}
