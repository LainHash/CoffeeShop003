using CoffeeShop.Domain.Entities.Catalog;

namespace CoffeeShop.Domain.Entities.Inventory
{
    public class ProductSku
    {
        public int ProductSkuId { get; set; }
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
