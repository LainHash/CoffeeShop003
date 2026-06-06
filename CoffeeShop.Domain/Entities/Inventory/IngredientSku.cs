using CoffeeShop.Domain.Common.Models;
using CoffeeShop.Domain.Entities.Catalog;

namespace CoffeeShop.Domain.Entities.Inventory
{
    public class IngredientSku : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Stock { get; set; }
        public string Status { get; set; } = null!;

        // Foreign Keys
        public int IngredientId { get; set; }

        // Navigation Properties
        public virtual Ingredient Ingredient { get; set; } = null!;
    }
}
