namespace BeelineMicroService.Models
{
    public class EventEntity
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public string Payload { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsProcessed { get; set; } = false;
    }
}
