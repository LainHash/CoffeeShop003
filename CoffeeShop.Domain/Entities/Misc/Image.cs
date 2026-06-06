using CoffeeShop.Domain.Common.Models;

namespace CoffeeShop.Domain.Entities.Misc
{
    public class Image : BaseEntity
    {
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }
        public int ReferenceId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
