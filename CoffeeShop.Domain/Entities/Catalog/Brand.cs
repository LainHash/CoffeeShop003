using CoffeeShop.Domain.Common.Models;

namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Brand : SoftDeleteEntity
    {
        public string Name { get; set; } = null!;

        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
