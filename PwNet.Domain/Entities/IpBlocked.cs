namespace PwNet.Domain.Entities
{
    public class IpBlocked : AuditableEntity
    {
        public required string IpAddress { get; set; }
        public required DateTime BlockedUntil { get; set; }

        public bool IsBlocked { get => BlockedUntil > DateTime.Now; }
    }
}