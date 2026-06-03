namespace CoffeeShop.Domain.Entities.Production.Recipes
{
    public class RecipeStep
    {
        public int RecipeStepId { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; } = null!;
        public int DurationSeconds { get; set; }

        // Navigation Properties
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
