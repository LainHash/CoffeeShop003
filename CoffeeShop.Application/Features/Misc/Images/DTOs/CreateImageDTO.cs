namespace CoffeeShop.Application.Features.Misc.Images.DTOs
{
    public class CreateImageDTO
    {
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }
    }
}
