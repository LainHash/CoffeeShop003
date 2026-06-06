

using CoffeeShop.Domain.Common.Models;

namespace CoffeeShop.Domain.Entities.Inventory
{
    public class StockTransaction : BaseEntity
    {
        public string Type { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public int ReferenceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
