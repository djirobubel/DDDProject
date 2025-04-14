namespace BeelineMicroService.Events
{
    public record BalanceAdded(int Id, decimal OldBalance, decimal NewBalance) : IEvent;
}
