namespace CoffeeShop.Application.Features.Catalog.Products.DTOs
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsMadeToOrder { get; set; }

        public string? BrandName { get; set; }
        public string CategoryName { get; set; } = null!;

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Stock { get; set; }
    }
}
