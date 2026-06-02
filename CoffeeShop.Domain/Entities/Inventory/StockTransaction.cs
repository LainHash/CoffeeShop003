using CoffeeShop.Domain.Enums;

namespace CoffeeShop.Domain.Entities.Inventory
{
    public class StockTransaction
    {
        public int StockTransactionId { get; set; }
        public ReferenceType Type { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public int ReferenceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
