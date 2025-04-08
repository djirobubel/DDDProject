namespace BeelineMicroService.EventBus
{
    public interface IEventBus
    {
        Task Publish<T>(T value);
    }
}
