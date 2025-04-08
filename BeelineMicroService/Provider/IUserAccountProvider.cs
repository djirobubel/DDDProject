namespace BeelineMicroService.Provider
{
    public interface IUserAccountProvider
    {
        Task<IUserAccountAggregate> GetAggregateAsync(int id);
        Task SaveAggregateAsync(IUserAccountAggregate aggregate);
    }
}
