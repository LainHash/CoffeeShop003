namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
