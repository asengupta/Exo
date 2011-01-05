namespace Exo.EventBus
{
    public interface IEventBusListener
    {
        // Methods
        void RegisterComponent(object component, string description);
        void IsMessaged(QueueResponse queueResponse);
    }
}