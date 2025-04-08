using BeelineMicroService.Context;
using BeelineMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace BeelineMicroService.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly UserAccountDbContext _context;

        public EventRepository(UserAccountDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(EventEntity entity)
        {
            _context.Events.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EventEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Events.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<EventEntity> GetOldestUnprocessedEventAsync()
        {
            return await _context.Events.Where(x => !x.IsProcessed).OrderBy(x => x.CreatedAt).FirstOrDefaultAsync();
        }
    }
}
