using BeelineMicroService.Events;
using BeelineMicroService.Repositories;
using MediatR;

namespace BeelineMicroService.EventHandlers
{
    public class EventHandler : INotificationHandler<BalanceAdded>, INotificationHandler<UserBlocked>
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public EventHandler(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task Handle(BalanceAdded notification, CancellationToken cancellationToken)
        {
            var userAccount = await _userAccountRepository.GetByIdAsync(notification.Id);
            if (!userAccount.IsBlocked)
            {
                userAccount.Balance += notification.Amount;
                userAccount.Version++;
                await _userAccountRepository.SaveAsync(userAccount);
            }
        }

        public async Task Handle(UserBlocked notification, CancellationToken cancellationToken)
        {
            var userAccount = await _userAccountRepository.GetByIdAsync(notification.Id);
            userAccount.IsBlocked = true;
            userAccount.Version++;
            await _userAccountRepository.SaveAsync(userAccount);
        }
    }
}
