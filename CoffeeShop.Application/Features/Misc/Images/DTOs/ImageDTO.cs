namespace CoffeeShop.Application.Features.Misc.Images.DTOs
{
    public class ImageDTO
    {
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
