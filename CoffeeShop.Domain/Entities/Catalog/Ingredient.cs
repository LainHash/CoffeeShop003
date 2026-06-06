using CoffeeShop.Domain.Common.Models;
using CoffeeShop.Domain.Entities.Inventory;
using CoffeeShop.Domain.Entities.Production.Recipes;

namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Ingredient : SoftDeleteEntity
    {
        public string Name { get; set; } = null!;

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
