using BeelineMicroService.EventBus;
using BeelineMicroService.Repositories;

namespace BeelineMicroService.Provider
{
    public class UserAccountProvider : IUserAccountProvider
    {
        private readonly IUserAccountRepository _repository;
        private readonly IEventBus _eventBus;

        public UserAccountProvider(IUserAccountRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<IUserAccountAggregate> GetAggregateAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var aggregate = new UserAccountAggregate(entity.Id, entity.Balance, entity.IsBlocked, entity.Version);
            return new UserAccountDecorator(aggregate, _eventBus);
        }

        public Task SaveAggregateAsync(IUserAccountAggregate aggregate)
        {
            // не используется, так как сохранение происходит через EventHandler
            Console.WriteLine("Данный метод - заглушка, так как сохранение происходит через EventHandler.");
            return Task.CompletedTask;
        }
    }
}
