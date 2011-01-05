using Exo.Aspects.Core;

namespace Exo.Aspects.ComponentQueue
{
    public class ReturnValueInMemoryPublication : IReturnValuePublication
    {
        public void Run(object returnValue, string description)
        {
            new SafeAspectExecution().Run(() => EventBus.ComponentEventBus.Instance.Publish(returnValue, description));
        }
    }
}