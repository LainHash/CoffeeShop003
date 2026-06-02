namespace CoffeeShop.Domain.Entities.Catalog
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? Description { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
