using Exo.Aspects.Core;

namespace Exo.Aspects.ComponentQueue
{
    public class SelfInMemoryPublication : ISelfPublication
    {
        public void Run(object self, string description)
        {
            new SafeAspectExecution().Run(() => EventBus.ComponentEventBus.Instance.Publish(self, description));
        }
    }
}