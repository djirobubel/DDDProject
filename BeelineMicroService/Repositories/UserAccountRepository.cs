using BeelineMicroService.Context;
using BeelineMicroService.Models;

namespace BeelineMicroService.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserAccountDbContext _context;

        public UserAccountRepository(UserAccountDbContext context)
        {
            _context = context;
        }

        public async Task<UserAccountEntity> GetByIdAsync(int id)
        {
            return await _context.UserAccounts.FindAsync(id)
                ?? throw new InvalidOperationException($"UserAccount с ID {id} не найден.");
        }

        public async Task SaveAsync(UserAccountEntity entity)
        {
            var existing = await _context.UserAccounts.FindAsync(entity.Id);
            if (existing == null)
            {
                _context.UserAccounts.Add(entity);
            }
            else
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
            }
            await _context.SaveChangesAsync();
        }
    }
}
