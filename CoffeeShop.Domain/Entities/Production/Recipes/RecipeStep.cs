using CoffeeShop.Domain.Common.Models;

namespace CoffeeShop.Domain.Entities.Production.Recipes
{
    public class RecipeStep : BaseEntity
    {
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public int DurationSeconds { get; set; }

        // Navigation Properties
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
