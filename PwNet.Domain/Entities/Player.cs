namespace PwNet.Domain.Entities
{
    public class Player : AuditableEntity
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? LastIpAddress { get; set; }
    }
}
