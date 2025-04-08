using BeelineMicroService.Models;

namespace BeelineMicroService.Repositories
{
    public interface IEventRepository
    {
        Task SaveAsync(EventEntity entity);
        Task UpdateAsync(EventEntity entity);
        Task<EventEntity> GetOldestUnprocessedEventAsync();
    }
}
