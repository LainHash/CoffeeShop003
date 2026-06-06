using CoffeeShop.Application.Features.Misc.Images.DTOs;

namespace CoffeeShop.Application.Features.Catalog.Products.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsMadeToOrder { get; set; }

        public string? BrandName { get; set; }
        public string CategoryName { get; set; } = null!;
        public List<CreateImageDTO> Images { get; set; } = new List<CreateImageDTO>();

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Stock { get; set; }
    }
}
