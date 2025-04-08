namespace BeelineMicroService.Models
{
    public class UserAccountEntity
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public bool IsBlocked { get; set; }
        public int Version { get; set; }
    }
}
