using CoffeeShop.Domain.Enums;

namespace CoffeeShop.Domain.Entities.Misc
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }
        public int ReferenceId { get; set; }
        public ReferenceType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
