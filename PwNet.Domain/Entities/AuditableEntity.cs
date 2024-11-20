namespace PwNet.Domain.Entities
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime InsertedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }
    }
}
