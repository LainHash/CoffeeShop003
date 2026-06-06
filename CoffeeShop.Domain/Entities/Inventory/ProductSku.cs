using CoffeeShop.Domain.Common.Models;
using CoffeeShop.Domain.Entities.Catalog;

namespace CoffeeShop.Domain.Entities.Inventory
{
    public class ProductSku : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Stock { get; set; }
        public string Status { get; set; } = null!;

        // Foreign Keys
        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Product Product { get; set; } = null!;
    }
}
