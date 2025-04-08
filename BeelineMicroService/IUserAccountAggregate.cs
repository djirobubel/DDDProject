namespace BeelineMicroService
{
    public interface IUserAccountAggregate
    {
        public int Id { get; }
        public long Version { get; }

        Task<AggregateResult> AddBalanceAsync(decimal amount);
        Task<AggregateResult> BlockUserAsync();
    }
}
