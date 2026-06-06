using CoffeeShop.Domain.Common.Models;
using CoffeeShop.Domain.Entities.Catalog;

namespace CoffeeShop.Domain.Entities.Production.Recipes
{
    public class Recipe : SoftDeleteEntity
    {
        public string Inspiration { get; set; } = null!;

        // Foreign Keys
        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();
    }
}
