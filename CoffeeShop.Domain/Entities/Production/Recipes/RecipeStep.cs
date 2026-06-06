using CoffeeShop.Domain.Common.Models;
using System.ComponentModel;

namespace CoffeeShop.Domain.Entities.Production.Recipes
{
    public class RecipeStep : BaseEntity
    {
        public int RecipeId { get; set; }
        public new string Description { get; set; } = null!;
        public int StepNumber { get; set; }
        public int DurationSeconds { get; set; }

        // Navigation Properties
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
