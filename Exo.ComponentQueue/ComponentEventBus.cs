using System.Collections.Generic;

namespace Exo.EventBus
{
    public class ComponentEventBus
    {
        // Fields
        public static readonly ComponentEventBus Instance = new ComponentEventBus();
        private readonly List<IEventBusListener> listeners = new List<IEventBusListener>();

        // Methods
        private ComponentEventBus()
        {
        }

        public void AddListener(IEventBusListener listener)
        {
            this.listeners.Add(listener);
        }

        public void Publish(object component)
        {
            this.listeners.ForEach(delegate (IEventBusListener listener) {
                                                                             listener.IsAvailable(component);
            });
        }

        public void PublishMessage(QueueResponse queueResponse)
        {
            this.listeners.ForEach(delegate (IEventBusListener listener) {
                                                                             listener.IsMessaged(queueResponse);
            });
        }
    }
}