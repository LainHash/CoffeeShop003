namespace CoffeeShop.Domain.Common.Models
{
    public class SoftDeleteEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
