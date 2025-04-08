namespace BeelineMicroService
{
    public class UserAccountAggregate : IUserAccountAggregate
    {
        public int Id { get; private set; }
        public long Version { get; private set; }
        public decimal Balance { get; private set; }
        public bool IsBlocked { get; private set; }

        public UserAccountAggregate(int id, decimal balance, bool isBlocked, int version)
        {
            Id = id;
            Balance = balance;
            IsBlocked = isBlocked;
            Version = version;
        }

        public Task<AggregateResult> AddBalanceAsync(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Сумма не может быть отрицательной.");
            if (IsBlocked)
                throw new InvalidOperationException("Невозможно обновить баланс заблокированного пользователя.");

            Balance += amount;
            Version++;
            return Task.FromResult(AggregateResult.Ok(Version, $"Баланс пополнен на {amount}."));
        }

        public Task<AggregateResult> BlockUserAsync()
        {
            IsBlocked = true;
            Version++;
            return Task.FromResult(AggregateResult.Ok(Version, $"Пользователь заблокирован"));
        }
    }
}
