namespace BeelineMicroService.Events
{
    public record UserBlocked(int Id) : IEvent;
}
