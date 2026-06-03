namespace CoffeeShop.Application.Features.Catalog.Products.DTOs
{
    public class ProductDTO
    {
        public Guid PublicId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsMadeToOrder { get; set; }

        public string? BrandName { get; set; }
        public string CategoryName { get; set; } = null!;

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Stock { get; set; }
        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
