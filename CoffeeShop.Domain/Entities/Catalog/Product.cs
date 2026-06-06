using CoffeeShop.Domain.Common.Models;
using CoffeeShop.Domain.Entities.Inventory;
using CoffeeShop.Domain.Entities.Production.Recipes;

namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Product : SoftDeleteEntity
    {
        public string Name { get; set; } = null!;
        public bool IsMadeToOrder { get; set; }

        // Foreign Keys
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }

        // Navigation Properties
        public virtual Category Category { get; set; } = null!;
        public virtual Brand? Brand { get; set; }
        public virtual ProductSku ProductSku { get; set; } = null!;
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
