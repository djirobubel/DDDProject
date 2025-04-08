using BeelineMicroService.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace BeelineMicroService.Worker
{
    public class EventProcessingWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public EventProcessingWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var eventEntity = await eventRepository.GetOldestUnprocessedEventAsync();
                    if (eventEntity == null)
                    {
                        await Task.Delay(1000, stoppingToken);
                        continue;
                    }

                    var eventType = Type.GetType($"BeelineMicroService.Events.{eventEntity.EventType}, BeelineMicroService");
                    if (eventType == null)
                    {
                        Console.WriteLine($"Тип события {eventEntity.EventType} не найден.");
                        eventEntity.IsProcessed = true;
                        await eventRepository.UpdateAsync(eventEntity);
                        continue;
                    }

                    var notification = JsonConvert.DeserializeObject(eventEntity.Payload, eventType) as INotification;
                    if (notification == null)
                    {
                        Console.WriteLine($"Не удалось десериализовать событие {eventEntity.EventType}. Payload: {eventEntity.Payload}");
                        eventEntity.IsProcessed = true;
                        await eventRepository.UpdateAsync(eventEntity);
                        continue;
                    }

                    await mediator.Publish(notification, stoppingToken);

                    eventEntity.IsProcessed = true;
                    await eventRepository.UpdateAsync(eventEntity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка в воркере: {ex.Message}");
                    await Task.Delay(5000, stoppingToken);
                }
            }
        }
    }
}
