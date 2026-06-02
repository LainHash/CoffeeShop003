using CoffeeShop.Domain.Entities.Catalog;

namespace CoffeeShop.Domain.Entities.Production.Recipes
{
    public class RecipeIngredient
    {
        public int RecipeIngredientId { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        // Navigation Properties
        public virtual Recipe Recipe { get; set; } = null!;
        public virtual Ingredient Ingredient { get; set; } = null!;
    }
}
