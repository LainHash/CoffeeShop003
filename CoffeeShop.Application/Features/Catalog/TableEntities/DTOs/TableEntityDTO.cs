namespace CoffeeShop.Application.Features.Catalog.TableEntities.DTOs
{
    public class TableEntityDTO
    {
        public Guid PublicId { get; set; }

        public string Shape { get; set; } = null!;

        public int TableNumber { get; set; }

        public int FloorNumber { get; set; }

        public int Capacity { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
