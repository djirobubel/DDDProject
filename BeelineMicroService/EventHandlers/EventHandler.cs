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
            if (notification.Id <= 0)
            {
                Console.WriteLine($"Некорректный Id {notification.Id} в событии BalanceAdded");
                return;
            }

            try
            {
                var userAccount = await _userAccountRepository.GetByIdAsync(notification.Id);
                if (!userAccount.IsBlocked)
                {
                   if (userAccount.Balance == notification.OldBalance)
                   {
                        userAccount.Balance = notification.NewBalance;
                        userAccount.Version++;
                        await _userAccountRepository.SaveAsync(userAccount);
                        Console.WriteLine($"Баланс обновлён: Id = {notification.Id}, OldBalance = {notification.OldBalance}, NewBalance = {notification.NewBalance}");
                   }
                    else
                    {
                        Console.WriteLine($"Конфликт баланса: Id = {notification.Id}, CurrentBalance = {userAccount.Balance}, OldBalance = {notification.OldBalance}. Событие проигнорировано.");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка обработки BalanceAdded: {ex.Message}");
            }
        }

        public async Task Handle(UserBlocked notification, CancellationToken cancellationToken)
        {
            if (notification.Id <= 0)
            {
                Console.WriteLine($"Некорректный Id {notification.Id} в событии UserBlocked.");
                return;
            }

            try
            {
                var userAccount = await _userAccountRepository.GetByIdAsync(notification.Id);
                if (!userAccount.IsBlocked) 
                {
                    userAccount.IsBlocked = true;
                    userAccount.Version++;
                    await _userAccountRepository.SaveAsync(userAccount);
                    Console.WriteLine($"Пользователь заблокирован: Id = {notification.Id}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка обработки UserBlocked: {ex.Message}");
            }
        }
    }
}
