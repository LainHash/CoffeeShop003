namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
