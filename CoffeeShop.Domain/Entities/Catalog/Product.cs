using CoffeeShop.Domain.Entities.Inventory;
using CoffeeShop.Domain.Entities.Production.Recipes;

namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Product
    {
        public int ProductId { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsMadeToOrder { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        // Foreign Keys
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }

        // Navigation Properties
        public virtual Category Category { get; set; } = null!;
        public virtual Brand? Brand { get; set; }
        public virtual ProductSku? ProductSku { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
