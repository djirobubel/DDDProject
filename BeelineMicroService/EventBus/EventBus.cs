using BeelineMicroService.Models;
using BeelineMicroService.Repositories;
using Newtonsoft.Json;

namespace BeelineMicroService.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IEventRepository _eventRepository;

        public EventBus(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Publish<T>(T value)
        {
            var eventType = typeof(T).Name;
            var payload = JsonConvert.SerializeObject(value);
            var eventEntity = new EventEntity
            {
                EventType = eventType,
                Payload = payload,
                CreatedAt = DateTime.UtcNow
            };

            await _eventRepository.SaveAsync(eventEntity);
        }
    }
}
