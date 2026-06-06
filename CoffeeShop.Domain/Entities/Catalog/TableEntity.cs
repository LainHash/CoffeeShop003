using CoffeeShop.Domain.Common.Models;

namespace CoffeeShop.Domain.Entities.Catalog
{
    public class TableEntity : SoftDeleteEntity
    {
        public string Shape { get; set; } = null!;

        public int TableNumber { get; set; }

        public int FloorNumber { get; set; }

        public int Capacity { get; set; }

        public string Status { get; set; } = null!;
    }
}
