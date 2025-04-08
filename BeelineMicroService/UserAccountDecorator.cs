using BeelineMicroService.EventBus;
using BeelineMicroService.Events;

namespace BeelineMicroService
{
    public class UserAccountDecorator : IUserAccountAggregate
    {
        private readonly IUserAccountAggregate _userAccountAggregate;
        private readonly IEventBus _eventBus;

        public UserAccountDecorator(IUserAccountAggregate userAccount, IEventBus eventBus)
        {
            _userAccountAggregate = userAccount;
            _eventBus = eventBus;
        }

        public int Id => _userAccountAggregate.Id;

        public long Version => _userAccountAggregate.Version;

        public async Task<AggregateResult> AddBalanceAsync(decimal amount)
        {
            var result = await _userAccountAggregate.AddBalanceAsync(amount);

            if (result.Success)
            {
                await _eventBus.Publish(new BalanceAdded(Id, amount));
            }

            return result;
        }

        public async Task<AggregateResult> BlockUserAsync()
        {
            var result = await _userAccountAggregate.BlockUserAsync();

            if (result.Success)
            {
                await _eventBus.Publish(new UserBlocked(Id));
            }

            return result;
        }
    }
}
