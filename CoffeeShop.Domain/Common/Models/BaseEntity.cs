namespace CoffeeShop.Domain.Common.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string? Description { get; set; }
    }
}
