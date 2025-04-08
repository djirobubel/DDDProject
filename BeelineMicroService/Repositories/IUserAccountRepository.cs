using BeelineMicroService.Models;

namespace BeelineMicroService.Repositories
{
    public interface IUserAccountRepository
    {
        Task<UserAccountEntity> GetByIdAsync(int id);
        Task SaveAsync(UserAccountEntity entity);
    }
}
