using BeelineMicroService.EventBus;
using BeelineMicroService.Repositories;
using MediatR;

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
            // сохранение отключено
            Console.WriteLine("Сохранение отключено до реализации обработчика событий.");
            return Task.CompletedTask;
        }
    }
}
