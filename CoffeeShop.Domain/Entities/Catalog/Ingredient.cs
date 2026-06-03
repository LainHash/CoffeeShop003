using CoffeeShop.Domain.Entities.Inventory;
using CoffeeShop.Domain.Entities.Production.Recipes;

namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string IngredientName { get; set; } = null!;
        public string? Description { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        // Foreign Keys
        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        // Navigation Properties
        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual IngredientSku IngredientSku { get; set; } = null!;
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
