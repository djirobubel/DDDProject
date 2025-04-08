namespace BeelineMicroService.Events
{
    public record BalanceAdded(int Id, decimal Amount) : IEvent;
}
