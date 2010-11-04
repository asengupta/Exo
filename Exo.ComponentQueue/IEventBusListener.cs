namespace Exo.EventBus
{
    public interface IEventBusListener
    {
        // Methods
        void IsAvailable(object component);
        void IsMessaged(QueueResponse queueResponse);
    }
}